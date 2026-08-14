# Plan de Trabajo: Aislamiento Multi-Tenant, RBAC Agenda y Auditoría General

---

## Contexto

Se detectaron fugas de datos entre talleres (tenants) y un fallo de permisos en el frontend de Agenda. Este plan detalla **exactamente** qué archivos se van a modificar, qué código se va a agregar, y qué script SQL se necesita ejecutar. No incluye trabajo ya realizado anteriormente.

---

## Resultado de la Auditoría de Modelos

Revisé las 44 entidades del dominio. Clasifiqué cada una en una de estas categorías:

### ✅ Entidades que YA tienen `WorkshopId` y `HasQueryFilter` (no requieren cambios)
| Entidad | Tipo de `WorkshopId` |
|---|---|
| `User` | `int?` |
| `UserRole` | `int?` |
| `Brand` | `int` |
| `BrandModels` | `int` |
| `BrandModelVersion` | `int` |
| `Product` | `int` |
| `ProductType` | `int` |
| `Customer` | `int` |
| `Vehicle` | `int` |
| `WorkOrder` | `int` |
| `PaymentMethod` | `int` |
| `Supplier` | `int` |
| `ServiceType` | `int` |
| `ServiceCatalog` | `int` |
| `ServicePriceByVersion` | `int` |
| `Inventory` | `int` |
| `InventoryReception` | `int` |
| `Appointment` | `int` |
| `Sale` | `int` |
| `WorkshopSettings` | `int` |
| `MechanicPaymentSettings` | `int` |
| `MechanicPaymentSettlement` | `int` |

### 🔴 Entidades que NO tienen `WorkshopId` y NECESITAN aislamiento (FUGAS ACTIVAS)
| Entidad | Tabla en DB | ¿Es catálogo independiente? | Estado |
|---|---|---|---|
| `IdentificationType` | `identification_type` | ✅ Sí — cada taller configura sus propios tipos de documento | **FUGA ACTIVA** |
| `AgendaSettings` | `agenda_settings` | ✅ Sí — cada taller tiene su propia configuración de agenda | **FUGA ACTIVA** |
| `AgendaBlock` | `agenda_block` | ✅ Sí — cada taller bloquea sus propias fechas | **FUGA ACTIVA** |
| `AgendaDayConfig` | `agenda_day_config` | ✅ Sí — cada taller configura cupos por día | **FUGA ACTIVA** |
| `EmailSettings` | `email_settings` | ✅ Sí — cada taller tiene su propio SMTP | **FUGA ACTIVA** |

### ⚪ Entidades que NO necesitan `WorkshopId` (son globales de plataforma o tablas hijas)
| Entidad | Razón |
|---|---|
| `Module` | Catálogo global de la plataforma (Clientes, Inventario, etc.). Todos los talleres usan los mismos módulos. |
| `Operation` | Catálogo global (GET, POST, PUT, DELETE). Igual para todos. |
| `Action` | Combina Module + Operation. Global de plataforma. |
| `RoleAction` | Tabla pivote de `UserRole` ↔ `Action`. Se aísla indirectamente porque `UserRole` ya tiene `WorkshopId`. |
| `UserRoleModule` | Tabla pivote de `UserRole` ↔ `Module`. Se aísla indirectamente. |
| `Login` | Historial de logins. Se aísla indirectamente porque tiene FK a `User` que ya está filtrado. |
| `PasswordResetToken` | FK a `User`, se aísla indirectamente. |
| `WorkOrderEvidence` | FK a `WorkOrder` (ya filtrada). |
| `WorkOrderHistory` | FK a `WorkOrder`. |
| `WorkOrderPart` | FK a `WorkOrder`. |
| `WorkOrderService` | FK a `WorkOrder`. |
| `SaleDetail` | FK a `Sale` (ya filtrada). |
| `SalePayment` | FK a `Sale`. |
| `InventoryHistory` | FK a `Inventory` (ya filtrada). |
| `InventoryReceptionDetail` | FK a `InventoryReception`. |
| `Workshop` | Es la tabla raíz del tenant, no se filtra a sí misma. |

---

## Cambios Propuestos

### FASE 1: Interceptor Centralizado de Tenant (Backend — `DataContext.cs`)

> [!IMPORTANT]
> Esto es la pieza clave. En vez de depender de que cada repositorio recuerde asignar `WorkshopId` manualmente (error humano), haremos que **EF Core lo haga automáticamente** al guardar cualquier entidad.

#### [MODIFY] [`DataContext.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Infrastructure/Data/DataContext.cs)

Sobreescribir `SaveChangesAsync` para que, en cada `INSERT` (entidad con `EntityState.Added`), busque si la entidad tiene la propiedad `WorkshopId`. Si la tiene y está en `null` o `0`, y el `CurrentTenantId > 0`, la asigna automáticamente.

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    if (CurrentTenantId > 0)
    {
        foreach (var entry in ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added))
        {
            var workshopProp = entry.Properties
                .FirstOrDefault(p => p.Metadata.Name == "WorkshopId");
            if (workshopProp != null)
            {
                var currentValue = workshopProp.CurrentValue;
                // Si es null, 0, o no fue asignado → inyectar tenant
                if (currentValue == null || 
                    (currentValue is int intVal && intVal == 0))
                {
                    workshopProp.CurrentValue = CurrentTenantId;
                }
            }
        }
    }
    return await base.SaveChangesAsync(cancellationToken);
}
```

**Impacto:** Soluciona la fuga de `UserRole` (roles que se creaban con `WorkshopId = null`) y previene fugas futuras en cualquier otra tabla.

---

### FASE 2: Agregar `WorkshopId` a las 5 entidades que lo necesitan

#### 2A. `IdentificationType`

##### [MODIFY] [`IdentificationType.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Domain/Models/IdentificationType.cs)
Agregar:
```csharp
public int? WorkshopId { get; set; }
public virtual Workshop? WorkshopNavigation { get; set; }
```

##### [MODIFY] [`IdentificacionTypeConfigurations.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Infrastructure/Data/Configurations/IdentificacionTypeConfigurations.cs)
Agregar la columna `workshop_id` y la relación FK con `Workshop`.

##### [MODIFY] [`DataContext.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Infrastructure/Data/DataContext.cs)
Agregar línea de `HasQueryFilter` para `IdentificationType`.

#### 2B. `AgendaSettings`

##### [MODIFY] [`AgendaSettings.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Domain/Models/AgendaSettings.cs)
Agregar `WorkshopId` + navegación.

##### [MODIFY] [`AgendaSettingsConfiguration.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Infrastructure/Data/Configurations/AgendaSettingsConfiguration.cs)
Agregar columna `workshop_id` y FK.

##### [MODIFY] `DataContext.cs` — Agregar `HasQueryFilter`.

#### 2C. `AgendaBlock`

##### [MODIFY] [`AgendaBlock.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Domain/Models/AgendaBlock.cs)
Agregar `WorkshopId` + navegación.

##### [MODIFY] [`AgendaBlockConfiguration.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Infrastructure/Data/Configurations/AgendaBlockConfiguration.cs)
Agregar columna `workshop_id` y FK.

##### [MODIFY] `DataContext.cs` — Agregar `HasQueryFilter`.

#### 2D. `AgendaDayConfig`

##### [MODIFY] [`AgendaDayConfig.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Domain/Models/AgendaDayConfig.cs)
Agregar `WorkshopId` + navegación.

##### [MODIFY] [`AgendaDayConfigConfiguration.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Infrastructure/Data/Configurations/AgendaDayConfigConfiguration.cs)
Agregar columna `workshop_id` y FK.

##### [MODIFY] `DataContext.cs` — Agregar `HasQueryFilter`.

#### 2E. `EmailSettings`

##### [MODIFY] [`EmailSettings.cs`](file:///c:/Users/miguelagutierrezg/Proyectos/Api/ApiTaller/ApiTaller.Domain/Models/EmailSettings.cs)
Agregar `WorkshopId` + navegación.

##### [MODIFY] `DataContext.cs` — Agregar `HasQueryFilter`.

---

### FASE 3: Script SQL para migrar la base de datos

#### [NEW] Script SQL `AddWorkshopIdToMissingTables.sql`

Este script:
1. Agrega la columna `workshop_id` a las 5 tablas (`identification_type`, `agenda_settings`, `agenda_block`, `agenda_day_config`, `email_settings`).
2. Asigna el `workshop_id` del taller existente a los registros actuales (para no perder datos).
3. Crea las Foreign Keys correspondientes.

> [!WARNING]
> Este script debe ejecutarse **antes** de reiniciar el backend con los cambios de C#. Si no, EF Core intentará filtrar por una columna que aún no existe en la base de datos.

---

### FASE 4: Corrección de RBAC en el Frontend de Agenda

#### [MODIFY] [`agenda-dashboard.ts`](file:///c:/Users/miguelagutierrezg/Proyectos/Front/TallerMotoApp/src/app/features/agenda/pages/agenda-dashboard/agenda-dashboard.ts)
- Inyectar `PermissionsService` y `Auth`.
- Exponer `PERMS = PERMISSIONS` como propiedad pública.

#### [MODIFY] [`agenda-dashboard.html`](file:///c:/Users/miguelagutierrezg/Proyectos/Front/TallerMotoApp/src/app/features/agenda/pages/agenda-dashboard/agenda-dashboard.html)
Línea 11-14 — El botón "Configuración" actualmente se muestra siempre:
```html
<button mat-flat-button class="primary-btn" routerLink="/home/operation/agenda/settings">
  <mat-icon>settings</mat-icon>
  Configuración
</button>
```
Envolverlo con:
```html
@if (permissions.hasPermission(PERMS.OPERATION.AGENDA.SETTINGS)) {
  <button mat-flat-button ...>...</button>
}
```

Líneas 110-115 — Botón "Recibir Moto" → Proteger con `PERMS.OPERATION.AGENDA.CONVERT`.

Líneas 123-128 — Botón "Cancelar" → Proteger con `PERMS.OPERATION.AGENDA.SAVE`.

---

### FASE 5: Impersonación del SuperAdmin

No requiere cambios. La lógica actual funciona correctamente:
- El `HasQueryFilter` usa: `(IsPlatformAdmin && CurrentTenantId == 0) || x.WorkshopId == CurrentTenantId`
- Cuando el SuperAdmin **no** ha elegido taller: `CurrentTenantId = 0`, `IsPlatformAdmin = true` → ve todo.
- Cuando el SuperAdmin **impersona** un taller: `CurrentTenantId = X` → ve solo datos del taller X.
- El interceptor de `SaveChangesAsync` (Fase 1) forzará que cualquier registro nuevo se guarde con el `WorkshopId` del taller impersonado.

---

## Resumen de Archivos a Modificar

| Capa | Archivo | Cambio |
|---|---|---|
| **Backend - Domain** | `IdentificationType.cs` | + `WorkshopId`, navegación |
| **Backend - Domain** | `AgendaSettings.cs` | + `WorkshopId`, navegación |
| **Backend - Domain** | `AgendaBlock.cs` | + `WorkshopId`, navegación |
| **Backend - Domain** | `AgendaDayConfig.cs` | + `WorkshopId`, navegación |
| **Backend - Domain** | `EmailSettings.cs` | + `WorkshopId`, navegación |
| **Backend - Infrastructure** | `DataContext.cs` | + `SaveChangesAsync` interceptor + 5 `HasQueryFilter` nuevos |
| **Backend - Infrastructure** | `IdentificacionTypeConfigurations.cs` | + columna `workshop_id` + FK |
| **Backend - Infrastructure** | `AgendaSettingsConfiguration.cs` | + columna `workshop_id` + FK |
| **Backend - Infrastructure** | `AgendaBlockConfiguration.cs` | + columna `workshop_id` + FK |
| **Backend - Infrastructure** | `AgendaDayConfigConfiguration.cs` | + columna `workshop_id` + FK |
| **Base de Datos** | `AddWorkshopIdToMissingTables.sql` | ALTER TABLE + UPDATE + FK (5 tablas) |
| **Frontend** | `agenda-dashboard.ts` | + inject `PermissionsService`, exponer `PERMS` |
| **Frontend** | `agenda-dashboard.html` | + 3 bloques `@if (permissions.hasPermission(...))` |

## Plan de Verificación

1. **Ejecutar script SQL** → Verificar en MySQL Workbench que las 5 tablas tienen la columna `workshop_id`.
2. **Recompilar backend** → Verificar que arranca sin errores.
3. **Probar aislamiento de Roles**: Crear un rol en "Deivid Motos", cambiar a otro taller y confirmar que no aparece.
4. **Probar aislamiento de Tipos de Identificación**: Crear "Pasaporte" en un taller, verificar que no existe en el otro.
5. **Probar RBAC Agenda**: Quitar el permiso `Configuracion_Agenda` a un rol, iniciar sesión con ese rol y verificar que el botón desaparece.
