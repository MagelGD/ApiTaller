### 2. Diccionario de Datos (Nivel DBA)

#### Entidad: **Action**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| ModuleId | int | NOT NULL | FK | Almacena el valor para ModuleId en el contexto de Action. |
| OperationId | int | NOT NULL | FK | Almacena el valor para OperationId en el contexto de Action. |
| Name | string | NULL |  | Almacena el valor para Name en el contexto de Action. |
| Slug | string | NULL |  | Almacena el valor para Slug en el contexto de Action. |
| ModuleIdNavigation | Module | NOT NULL |  | Almacena el valor para ModuleIdNavigation en el contexto de Action. |
| OperationIdNavigation | Operation | NOT NULL |  | Almacena el valor para OperationIdNavigation en el contexto de Action. |

#### Entidad: **AgendaBlock**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| BlockDate | DateTime | NOT NULL |  | Almacena el valor para BlockDate en el contexto de AgendaBlock. |
| Reason | string | NULL |  | Almacena el valor para Reason en el contexto de AgendaBlock. |

#### Entidad: **AgendaDayConfig**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Date | DateTime | NOT NULL |  | Almacena el valor para Date en el contexto de AgendaDayConfig. |
| CustomSlots | int? | NULL |  | Almacena el valor para CustomSlots en el contexto de AgendaDayConfig. |
| IsBlocked | bool | NOT NULL |  | Almacena el valor para IsBlocked en el contexto de AgendaDayConfig. |
| Reason | string? | NULL |  | Almacena el valor para Reason en el contexto de AgendaDayConfig. |

#### Entidad: **AgendaSettings**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| WeeksToOpen | int | NOT NULL |  | Almacena el valor para WeeksToOpen en el contexto de AgendaSettings. |
| DailySlots | int | NOT NULL |  | Almacena el valor para DailySlots en el contexto de AgendaSettings. |
| BusinessHoursStart | TimeSpan | NOT NULL |  | Almacena el valor para BusinessHoursStart en el contexto de AgendaSettings. |
| BusinessHoursEnd | TimeSpan | NOT NULL |  | Almacena el valor para BusinessHoursEnd en el contexto de AgendaSettings. |
| StartDate | DateTime | NOT NULL |  | Almacena el valor para StartDate en el contexto de AgendaSettings. |
| WorkingDays | string? | NULL |  | Almacena el valor para WorkingDays en el contexto de AgendaSettings. |

#### Entidad: **Appointment**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| CustomerId | int? | NULL | FK | Almacena el valor para CustomerId en el contexto de Appointment. |
| VehicleId | int? | NULL | FK | Almacena el valor para VehicleId en el contexto de Appointment. |
| ServiceTypeId | int? | NULL | FK | Almacena el valor para ServiceTypeId en el contexto de Appointment. |
| AppointmentDate | DateTime | NOT NULL |  | Almacena el valor para AppointmentDate en el contexto de Appointment. |
| AppointmentTime | TimeSpan? | NULL |  | Almacena el valor para AppointmentTime en el contexto de Appointment. |
| CustomerNotes | string | NULL |  | Almacena el valor para CustomerNotes en el contexto de Appointment. |
| Status | string | NULL |  | Almacena el valor para Status en el contexto de Appointment. |
| BookingSource | string | NULL |  | Almacena el valor para BookingSource en el contexto de Appointment. |
| WorkOrderId | int? | NULL | FK | Almacena el valor para WorkOrderId en el contexto de Appointment. |
| ContactName | string? | NULL |  | Almacena el valor para ContactName en el contexto de Appointment. |
| ContactPhone | string? | NULL |  | Almacena el valor para ContactPhone en el contexto de Appointment. |
| ContactEmail | string? | NULL |  | Almacena el valor para ContactEmail en el contexto de Appointment. |
| VehicleDescription | string? | NULL |  | Almacena el valor para VehicleDescription en el contexto de Appointment. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Appointment. |
| CustomerNavigation | Customer | NOT NULL |  | Almacena el valor para CustomerNavigation en el contexto de Appointment. |
| VehicleNavigation | Vehicle | NOT NULL |  | Almacena el valor para VehicleNavigation en el contexto de Appointment. |
| ServiceTypeNavigation | ServiceType | NOT NULL |  | Almacena el valor para ServiceTypeNavigation en el contexto de Appointment. |
| WorkOrderNavigation | WorkOrder | NOT NULL |  | Almacena el valor para WorkOrderNavigation en el contexto de Appointment. |

#### Entidad: **Brand**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Name | string | NULL |  | Almacena el valor para Name en el contexto de Brand. |
| VehicleType | string | NULL |  | Almacena el valor para VehicleType en el contexto de Brand. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Brand. |

#### Entidad: **BrandModels**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Models | string | NULL |  | Almacena el valor para Models en el contexto de BrandModels. |
| VehicleType | string | NULL |  | Almacena el valor para VehicleType en el contexto de BrandModels. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de BrandModels. |

#### Entidad: **BrandModelVersion**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| BrandId | int | NOT NULL | FK | Almacena el valor para BrandId en el contexto de BrandModelVersion. |
| ModelId | int | NOT NULL | FK | Almacena el valor para ModelId en el contexto de BrandModelVersion. |
| Version | string | NULL |  | Almacena el valor para Version en el contexto de BrandModelVersion. |
| VehicleType | string | NULL |  | Almacena el valor para VehicleType en el contexto de BrandModelVersion. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de BrandModelVersion. |
| Brand | Brand | NOT NULL |  | Almacena el valor para Brand en el contexto de BrandModelVersion. |
| Model | BrandModels | NOT NULL |  | Almacena el valor para Model en el contexto de BrandModelVersion. |

#### Entidad: **Customer**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| UserId | int | NOT NULL | FK | Almacena el valor para UserId en el contexto de Customer. |
| IdentificationTypeId | int | NOT NULL | FK | Almacena el valor para IdentificationTypeId en el contexto de Customer. |
| IdentificationNumber | string | NULL |  | Almacena el valor para IdentificationNumber en el contexto de Customer. |
| FirstName | string | NULL |  | Almacena el valor para FirstName en el contexto de Customer. |
| LastName | string | NULL |  | Almacena el valor para LastName en el contexto de Customer. |
| PhoneNumber | string | NULL |  | Almacena el valor para PhoneNumber en el contexto de Customer. |
| Email | string | NULL |  | Almacena el valor para Email en el contexto de Customer. |
| Address | string | NULL |  | Almacena el valor para Address en el contexto de Customer. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Customer. |
| UserIdNavigation | User | NOT NULL |  | Almacena el valor para UserIdNavigation en el contexto de Customer. |
| IdentificationTypeNavigation | IdentificationType | NOT NULL |  | Almacena el valor para IdentificationTypeNavigation en el contexto de Customer. |
| Vehicles | ICollection<Vehicle> | NOT NULL |  | Almacena el valor para Vehicles en el contexto de Customer. |

#### Entidad: **EmailSettings**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Host | string | NULL |  | Almacena el valor para Host en el contexto de EmailSettings. |
| Port | int | NOT NULL |  | Almacena el valor para Port en el contexto de EmailSettings. |
| UserName | string | NULL |  | Almacena el valor para UserName en el contexto de EmailSettings. |
| Password | string | NULL |  | Almacena el valor para Password en el contexto de EmailSettings. |
| EnableSsl | bool | NOT NULL |  | Almacena el valor para EnableSsl en el contexto de EmailSettings. |
| SenderName | string | NULL |  | Almacena el valor para SenderName en el contexto de EmailSettings. |
| SenderEmail | string | NULL |  | Almacena el valor para SenderEmail en el contexto de EmailSettings. |

#### Entidad: **GeneralEntity**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Id | int | NOT NULL | PK | Almacena el valor para Id en el contexto de GeneralEntity. |
| IsActive | bool | NOT NULL |  | Almacena el valor para IsActive en el contexto de GeneralEntity. |
| CreatedAt | DateTime | NOT NULL |  | Almacena el valor para CreatedAt en el contexto de GeneralEntity. |
| UpdatedAt | DateTime? | NULL |  | Almacena el valor para UpdatedAt en el contexto de GeneralEntity. |
| ResponsibleUserId | int? | NULL | FK | Almacena el valor para ResponsibleUserId en el contexto de GeneralEntity. |
| ResponsibleUserIdNavigation | User | NOT NULL |  | Almacena el valor para ResponsibleUserIdNavigation en el contexto de GeneralEntity. |

#### Entidad: **IdentificationType**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Identification | string | NULL |  | Almacena el valor para Identification en el contexto de IdentificationType. |

#### Entidad: **Inventory**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| ProductId | int | NOT NULL | FK | Almacena el valor para ProductId en el contexto de Inventory. |
| StockQuantity | int | NOT NULL |  | Almacena el valor para StockQuantity en el contexto de Inventory. |
| MinStock | int | NOT NULL |  | Almacena el valor para MinStock en el contexto de Inventory. |
| LastUpdate | DateTime | NOT NULL |  | Almacena el valor para LastUpdate en el contexto de Inventory. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Inventory. |
| ProductNavigation | Product | NOT NULL |  | Almacena el valor para ProductNavigation en el contexto de Inventory. |

#### Entidad: **InventoryHistory**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| ProductId | int | NOT NULL | FK | Almacena el valor para ProductId en el contexto de InventoryHistory. |
| MovementType | string | NULL |  | Almacena el valor para MovementType en el contexto de InventoryHistory. |
| Quantity | int | NOT NULL |  | Almacena el valor para Quantity en el contexto de InventoryHistory. |
| ReferenceId | int? | NULL | FK | Almacena el valor para ReferenceId en el contexto de InventoryHistory. |
| SupplierId | int? | NULL | FK | Almacena el valor para SupplierId en el contexto de InventoryHistory. |
| Observations | string | NULL |  | Almacena el valor para Observations en el contexto de InventoryHistory. |
| UnitCost | decimal? | NULL |  | Almacena el valor para UnitCost en el contexto de InventoryHistory. |
| SalePrice | decimal? | NULL |  | Almacena el valor para SalePrice en el contexto de InventoryHistory. |
| ProductNavigation | Product | NOT NULL |  | Almacena el valor para ProductNavigation en el contexto de InventoryHistory. |
| SupplierNavigation | Supplier | NOT NULL |  | Almacena el valor para SupplierNavigation en el contexto de InventoryHistory. |

#### Entidad: **InventoryReception**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| SupplierId | int? | NULL | FK | Almacena el valor para SupplierId en el contexto de InventoryReception. |
| ReceptionDate | DateTime | NOT NULL |  | Almacena el valor para ReceptionDate en el contexto de InventoryReception. |
| InvoiceImageBase64 | string | NULL |  | Almacena el valor para InvoiceImageBase64 en el contexto de InventoryReception. |
| Observations | string | NULL |  | Almacena el valor para Observations en el contexto de InventoryReception. |
| TotalAmount | decimal | NOT NULL |  | Almacena el valor para TotalAmount en el contexto de InventoryReception. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de InventoryReception. |
| SupplierNavigation | Supplier | NOT NULL |  | Almacena el valor para SupplierNavigation en el contexto de InventoryReception. |
| Details | ICollection<InventoryReceptionDetail> | NOT NULL |  | Almacena el valor para Details en el contexto de InventoryReception. |

#### Entidad: **InventoryReceptionDetail**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Id | int | NOT NULL | PK | Almacena el valor para Id en el contexto de InventoryReceptionDetail. |
| ReceptionId | int | NOT NULL | FK | Almacena el valor para ReceptionId en el contexto de InventoryReceptionDetail. |
| ProductId | int | NOT NULL | FK | Almacena el valor para ProductId en el contexto de InventoryReceptionDetail. |
| Quantity | int | NOT NULL |  | Almacena el valor para Quantity en el contexto de InventoryReceptionDetail. |
| UnitCost | decimal | NOT NULL |  | Almacena el valor para UnitCost en el contexto de InventoryReceptionDetail. |
| SalePrice | decimal | NOT NULL |  | Almacena el valor para SalePrice en el contexto de InventoryReceptionDetail. |
| ReceptionNavigation | InventoryReception | NOT NULL |  | Almacena el valor para ReceptionNavigation en el contexto de InventoryReceptionDetail. |
| ProductNavigation | Product | NOT NULL |  | Almacena el valor para ProductNavigation en el contexto de InventoryReceptionDetail. |

#### Entidad: **Login**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| UserId | int | NOT NULL | FK | Almacena el valor para UserId en el contexto de Login. |
| Message | string | NULL |  | Almacena el valor para Message en el contexto de Login. |

#### Entidad: **MechanicPaymentSettings**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| MechanicId | int | NOT NULL | FK | Almacena el valor para MechanicId en el contexto de MechanicPaymentSettings. |
| PaymentType | string | NULL |  | Almacena el valor para PaymentType en el contexto de MechanicPaymentSettings. |
| Value | decimal | NOT NULL |  | Almacena el valor para Value en el contexto de MechanicPaymentSettings. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de MechanicPaymentSettings. |
| MechanicNavigation | User | NOT NULL |  | Almacena el valor para MechanicNavigation en el contexto de MechanicPaymentSettings. |

#### Entidad: **MechanicPaymentSettlement**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| MechanicId | int | NOT NULL | FK | Almacena el valor para MechanicId en el contexto de MechanicPaymentSettlement. |
| SettlementDate | DateTime | NOT NULL |  | Almacena el valor para SettlementDate en el contexto de MechanicPaymentSettlement. |
| TotalAmount | decimal | NOT NULL |  | Almacena el valor para TotalAmount en el contexto de MechanicPaymentSettlement. |
| ServicesCount | int | NOT NULL |  | Almacena el valor para ServicesCount en el contexto de MechanicPaymentSettlement. |
| StartDate | DateTime | NOT NULL |  | Almacena el valor para StartDate en el contexto de MechanicPaymentSettlement. |
| EndDate | DateTime | NOT NULL |  | Almacena el valor para EndDate en el contexto de MechanicPaymentSettlement. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de MechanicPaymentSettlement. |
| MechanicNavigation | User | NOT NULL |  | Almacena el valor para MechanicNavigation en el contexto de MechanicPaymentSettlement. |
| Services | ICollection<WorkOrderService> | NOT NULL |  | Almacena el valor para Services en el contexto de MechanicPaymentSettlement. |

#### Entidad: **Module**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Name | string | NULL |  | Almacena el valor para Name en el contexto de Module. |

#### Entidad: **Operation**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Name | string | NULL |  | Almacena el valor para Name en el contexto de Operation. |

#### Entidad: **PasswordResetToken**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| UserId | int | NOT NULL | FK | Almacena el valor para UserId en el contexto de PasswordResetToken. |
| Token | string | NULL |  | Almacena el valor para Token en el contexto de PasswordResetToken. |
| ExpirationDate | DateTime | NOT NULL |  | Almacena el valor para ExpirationDate en el contexto de PasswordResetToken. |
| IsUsed | bool | NOT NULL |  | Almacena el valor para IsUsed en el contexto de PasswordResetToken. |
| User | User | NOT NULL |  | Almacena el valor para User en el contexto de PasswordResetToken. |

#### Entidad: **PaymentMethod**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Name | string | NULL |  | Almacena el valor para Name en el contexto de PaymentMethod. |
| Icon | string | NULL |  | Almacena el valor para Icon en el contexto de PaymentMethod. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de PaymentMethod. |

#### Entidad: **Product**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| ProducTypeId | int | NOT NULL | FK | Almacena el valor para ProducTypeId en el contexto de Product. |
| ProductName | string | NULL |  | Almacena el valor para ProductName en el contexto de Product. |
| Price | decimal | NOT NULL |  | Almacena el valor para Price en el contexto de Product. |
| SalePrice | decimal | NOT NULL |  | Almacena el valor para SalePrice en el contexto de Product. |
| Code | string | NULL |  | Almacena el valor para Code en el contexto de Product. |
| Reference | string | NULL |  | Almacena el valor para Reference en el contexto de Product. |
| Description | string | NULL |  | Almacena el valor para Description en el contexto de Product. |
| VehicleType | string | NULL |  | Almacena el valor para VehicleType en el contexto de Product. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Product. |
| ProductTypeIdNavigation | ProductType | NOT NULL |  | Almacena el valor para ProductTypeIdNavigation en el contexto de Product. |

#### Entidad: **ProductType**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Type | string | NULL |  | Almacena el valor para Type en el contexto de ProductType. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de ProductType. |

#### Entidad: **RoleAction**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| RoleId | int | NOT NULL | FK | Almacena el valor para RoleId en el contexto de RoleAction. |
| ActionId | int | NOT NULL | FK | Almacena el valor para ActionId en el contexto de RoleAction. |
| ActionIdNavigation | Action | NOT NULL |  | Almacena el valor para ActionIdNavigation en el contexto de RoleAction. |
| RoleIdNavigation | UserRole | NOT NULL |  | Almacena el valor para RoleIdNavigation en el contexto de RoleAction. |

#### Entidad: **Sale**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| WorkOrderId | int? | NULL | FK | Almacena el valor para WorkOrderId en el contexto de Sale. |
| CustomerId | int | NOT NULL | FK | Almacena el valor para CustomerId en el contexto de Sale. |
| SaleDate | DateTime | NOT NULL |  | Almacena el valor para SaleDate en el contexto de Sale. |
| Subtotal | decimal | NOT NULL |  | Almacena el valor para Subtotal en el contexto de Sale. |
| DiscountPercent | decimal | NOT NULL |  | Almacena el valor para DiscountPercent en el contexto de Sale. |
| DiscountAmount | decimal | NOT NULL |  | Almacena el valor para DiscountAmount en el contexto de Sale. |
| Total | decimal | NOT NULL |  | Almacena el valor para Total en el contexto de Sale. |
| DownPayment | decimal | NOT NULL |  | Almacena el valor para DownPayment en el contexto de Sale. |
| Balance | decimal | NOT NULL |  | Almacena el valor para Balance en el contexto de Sale. |
| Observations | string | NULL |  | Almacena el valor para Observations en el contexto de Sale. |
| WorkshopName | string? | NULL |  | Almacena el valor para WorkshopName en el contexto de Sale. |
| WorkshopSlogan | string? | NULL |  | Almacena el valor para WorkshopSlogan en el contexto de Sale. |
| LogoBase64 | string? | NULL |  | Almacena el valor para LogoBase64 en el contexto de Sale. |
| LogoBrandsBase64 | string? | NULL |  | Almacena el valor para LogoBrandsBase64 en el contexto de Sale. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Sale. |
| WorkOrder | WorkOrder | NOT NULL |  | Almacena el valor para WorkOrder en el contexto de Sale. |
| Customer | Customer | NOT NULL |  | Almacena el valor para Customer en el contexto de Sale. |
| Details | ICollection<SaleDetail> | NOT NULL |  | Almacena el valor para Details en el contexto de Sale. |
| Payments | ICollection<SalePayment> | NOT NULL |  | Almacena el valor para Payments en el contexto de Sale. |

#### Entidad: **SaleDetail**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| SaleId | int | NOT NULL | FK | Almacena el valor para SaleId en el contexto de SaleDetail. |
| ProductId | int? | NULL | FK | Almacena el valor para ProductId en el contexto de SaleDetail. |
| ServiceCatalogId | int? | NULL | FK | Almacena el valor para ServiceCatalogId en el contexto de SaleDetail. |
| Description | string | NULL |  | Almacena el valor para Description en el contexto de SaleDetail. |
| Quantity | int | NOT NULL |  | Almacena el valor para Quantity en el contexto de SaleDetail. |
| UnitPrice | decimal | NOT NULL |  | Almacena el valor para UnitPrice en el contexto de SaleDetail. |
| Total | decimal | NOT NULL |  | Almacena el valor para Total en el contexto de SaleDetail. |
| Sale | Sale | NOT NULL |  | Almacena el valor para Sale en el contexto de SaleDetail. |
| Product | Product | NOT NULL |  | Almacena el valor para Product en el contexto de SaleDetail. |
| Service | ServiceCatalog | NOT NULL |  | Almacena el valor para Service en el contexto de SaleDetail. |

#### Entidad: **SalePayment**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| SaleId | int | NOT NULL | FK | Almacena el valor para SaleId en el contexto de SalePayment. |
| PaymentMethodId | int | NOT NULL | FK | Almacena el valor para PaymentMethodId en el contexto de SalePayment. |
| Amount | decimal | NOT NULL |  | Almacena el valor para Amount en el contexto de SalePayment. |
| ReferenceCode | string | NULL |  | Almacena el valor para ReferenceCode en el contexto de SalePayment. |
| Sale | Sale | NOT NULL |  | Almacena el valor para Sale en el contexto de SalePayment. |
| PaymentMethod | PaymentMethod | NOT NULL |  | Almacena el valor para PaymentMethod en el contexto de SalePayment. |

#### Entidad: **ServiceCatalog**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| ServiceTypeId | int | NOT NULL | FK | Almacena el valor para ServiceTypeId en el contexto de ServiceCatalog. |
| Name | string | NULL |  | Almacena el valor para Name en el contexto de ServiceCatalog. |
| Description | string | NULL |  | Almacena el valor para Description en el contexto de ServiceCatalog. |
| DefaultPrice | decimal | NOT NULL |  | Almacena el valor para DefaultPrice en el contexto de ServiceCatalog. |
| DefaultMinutes | int | NOT NULL |  | Almacena el valor para DefaultMinutes en el contexto de ServiceCatalog. |
| TimeUnit | string? | NULL |  | Almacena el valor para TimeUnit en el contexto de ServiceCatalog. |
| VehicleType | string | NULL |  | Almacena el valor para VehicleType en el contexto de ServiceCatalog. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de ServiceCatalog. |
| ServiceTypeIdNavigation | ServiceType | NOT NULL |  | Almacena el valor para ServiceTypeIdNavigation en el contexto de ServiceCatalog. |

#### Entidad: **ServicePriceByVersion**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| ServiceCatalogId | int | NOT NULL | FK | Almacena el valor para ServiceCatalogId en el contexto de ServicePriceByVersion. |
| BrandModelVersionId | int | NOT NULL | FK | Almacena el valor para BrandModelVersionId en el contexto de ServicePriceByVersion. |
| Price | decimal | NOT NULL |  | Almacena el valor para Price en el contexto de ServicePriceByVersion. |
| EstimatedMinutes | int | NOT NULL |  | Almacena el valor para EstimatedMinutes en el contexto de ServicePriceByVersion. |
| TimeUnit | string? | NULL |  | Almacena el valor para TimeUnit en el contexto de ServicePriceByVersion. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de ServicePriceByVersion. |
| ServiceCatalogIdNavigation | ServiceCatalog | NOT NULL |  | Almacena el valor para ServiceCatalogIdNavigation en el contexto de ServicePriceByVersion. |
| BrandModelVersionIdNavigation | BrandModelVersion | NOT NULL |  | Almacena el valor para BrandModelVersionIdNavigation en el contexto de ServicePriceByVersion. |

#### Entidad: **ServiceType**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Name | string | NULL |  | Almacena el valor para Name en el contexto de ServiceType. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de ServiceType. |

#### Entidad: **Supplier**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| DocumentNumber | string | NULL |  | Almacena el valor para DocumentNumber en el contexto de Supplier. |
| BusinessName | string | NULL |  | Almacena el valor para BusinessName en el contexto de Supplier. |
| ContactName | string | NULL |  | Almacena el valor para ContactName en el contexto de Supplier. |
| PhoneNumber | string | NULL |  | Almacena el valor para PhoneNumber en el contexto de Supplier. |
| Email | string | NULL |  | Almacena el valor para Email en el contexto de Supplier. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Supplier. |

#### Entidad: **User**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Id | int | NOT NULL | PK | Almacena el valor para Id en el contexto de User. |
| WorkshopId | int? | NULL | FK | Almacena el valor para WorkshopId en el contexto de User. |
| UserRoleId | int | NOT NULL | FK | Almacena el valor para UserRoleId en el contexto de User. |
| IdentificationTypeId | int | NOT NULL | FK | Almacena el valor para IdentificationTypeId en el contexto de User. |
| IdentificationNumber | string | NULL |  | Almacena el valor para IdentificationNumber en el contexto de User. |
| FirstName | string | NULL |  | Almacena el valor para FirstName en el contexto de User. |
| MiddleName | string | NULL |  | Almacena el valor para MiddleName en el contexto de User. |
| FirstSurname | string | NULL |  | Almacena el valor para FirstSurname en el contexto de User. |
| SecondLastName | string | NULL |  | Almacena el valor para SecondLastName en el contexto de User. |
| FullName | string | NULL |  | Almacena el valor para FullName en el contexto de User. |
| Username | string | NULL |  | Almacena el valor para Username en el contexto de User. |
| Password | string | NULL |  | Almacena el valor para Password en el contexto de User. |
| Email | string | NULL |  | Almacena el valor para Email en el contexto de User. |
| Token | string? | NULL |  | Almacena el valor para Token en el contexto de User. |
| AssignmentDate | DateTime? | NULL |  | Almacena el valor para AssignmentDate en el contexto de User. |
| ExpirationDate | DateTime? | NULL |  | Almacena el valor para ExpirationDate en el contexto de User. |
| IsActive | bool | NOT NULL |  | Almacena el valor para IsActive en el contexto de User. |
| MustChangePassword | bool | NOT NULL |  | Almacena el valor para MustChangePassword en el contexto de User. |
| CreatedAt | DateTime | NOT NULL |  | Almacena el valor para CreatedAt en el contexto de User. |
| UpdatedAt | DateTime? | NULL |  | Almacena el valor para UpdatedAt en el contexto de User. |
| UserRoleIdNavigation | UserRole | NOT NULL |  | Almacena el valor para UserRoleIdNavigation en el contexto de User. |
| IdentificationTypeIdNavigation | IdentificationType | NOT NULL |  | Almacena el valor para IdentificationTypeIdNavigation en el contexto de User. |
| WorkshopNavigation | Workshop? | NULL |  | Almacena el valor para WorkshopNavigation en el contexto de User. |

#### Entidad: **UserRole**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Role | string | NULL |  | Almacena el valor para Role en el contexto de UserRole. |
| WorkshopId | int? | NULL | FK | Almacena el valor para WorkshopId en el contexto de UserRole. |
| WorkshopNavigation | Workshop? | NULL |  | Almacena el valor para WorkshopNavigation en el contexto de UserRole. |

#### Entidad: **UserRoleModule**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| UserRoleId | int | NOT NULL | FK | Almacena el valor para UserRoleId en el contexto de UserRoleModule. |
| ModulesRoleId | int | NOT NULL | FK | Almacena el valor para ModulesRoleId en el contexto de UserRoleModule. |
| ModuleIdNavigation | Module | NOT NULL |  | Almacena el valor para ModuleIdNavigation en el contexto de UserRoleModule. |
| UserRoleIdNavigation | UserRole | NOT NULL |  | Almacena el valor para UserRoleIdNavigation en el contexto de UserRoleModule. |

#### Entidad: **Vehicle**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| CustomerId | int | NOT NULL | FK | Almacena el valor para CustomerId en el contexto de Vehicle. |
| Plate | string | NULL |  | Almacena el valor para Plate en el contexto de Vehicle. |
| BrandId | int | NOT NULL | FK | Almacena el valor para BrandId en el contexto de Vehicle. |
| ModelId | int | NOT NULL | FK | Almacena el valor para ModelId en el contexto de Vehicle. |
| VersionId | int? | NULL | FK | Almacena el valor para VersionId en el contexto de Vehicle. |
| Color | string | NULL |  | Almacena el valor para Color en el contexto de Vehicle. |
| CylinderCapacity | string | NULL |  | Almacena el valor para CylinderCapacity en el contexto de Vehicle. |
| VehicleType | string | NULL |  | Almacena el valor para VehicleType en el contexto de Vehicle. |
| VehicleSubType | string? | NULL |  | Almacena el valor para VehicleSubType en el contexto de Vehicle. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de Vehicle. |
| CustomerNavigation | Customer | NOT NULL |  | Almacena el valor para CustomerNavigation en el contexto de Vehicle. |
| BrandNavigation | Brand | NOT NULL |  | Almacena el valor para BrandNavigation en el contexto de Vehicle. |
| ModelNavigation | BrandModels | NOT NULL |  | Almacena el valor para ModelNavigation en el contexto de Vehicle. |
| VersionNavigation | BrandModelVersion | NOT NULL |  | Almacena el valor para VersionNavigation en el contexto de Vehicle. |

#### Entidad: **WorkOrder**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| VehicleId | int | NOT NULL | FK | Almacena el valor para VehicleId en el contexto de WorkOrder. |
| CustomerId | int | NOT NULL | FK | Almacena el valor para CustomerId en el contexto de WorkOrder. |
| EntryDate | DateTime | NOT NULL |  | Almacena el valor para EntryDate en el contexto de WorkOrder. |
| EstimatedDeliveryDate | DateTime? | NULL |  | Almacena el valor para EstimatedDeliveryDate en el contexto de WorkOrder. |
| Mileage | int | NOT NULL |  | Almacena el valor para Mileage en el contexto de WorkOrder. |
| FuelLevel | string | NULL |  | Almacena el valor para FuelLevel en el contexto de WorkOrder. |
| Observations | string | NULL |  | Almacena el valor para Observations en el contexto de WorkOrder. |
| Status | string | NULL |  | Almacena el valor para Status en el contexto de WorkOrder. |
| DownPayment | decimal | NOT NULL |  | Almacena el valor para DownPayment en el contexto de WorkOrder. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de WorkOrder. |
| VehicleNavigation | Vehicle | NOT NULL |  | Almacena el valor para VehicleNavigation en el contexto de WorkOrder. |
| CustomerNavigation | Customer | NOT NULL |  | Almacena el valor para CustomerNavigation en el contexto de WorkOrder. |
| Evidences | ICollection<WorkOrderEvidence> | NOT NULL |  | Almacena el valor para Evidences en el contexto de WorkOrder. |
| Parts | ICollection<WorkOrderPart> | NOT NULL |  | Almacena el valor para Parts en el contexto de WorkOrder. |
| Services | ICollection<WorkOrderService> | NOT NULL |  | Almacena el valor para Services en el contexto de WorkOrder. |

#### Entidad: **WorkOrderEvidence**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| WorkOrderId | int | NOT NULL | FK | Almacena el valor para WorkOrderId en el contexto de WorkOrderEvidence. |
| PhotoUrl | string | NULL |  | Almacena el valor para PhotoUrl en el contexto de WorkOrderEvidence. |
| EvidenceType | string | NULL |  | Almacena el valor para EvidenceType en el contexto de WorkOrderEvidence. |
| Description | string | NULL |  | Almacena el valor para Description en el contexto de WorkOrderEvidence. |
| WorkOrderNavigation | WorkOrder | NOT NULL |  | Almacena el valor para WorkOrderNavigation en el contexto de WorkOrderEvidence. |

#### Entidad: **WorkOrderHistory**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| WorkOrderId | int | NOT NULL | FK | Almacena el valor para WorkOrderId en el contexto de WorkOrderHistory. |
| Status | string | NULL |  | Almacena el valor para Status en el contexto de WorkOrderHistory. |
| Observations | string | NULL |  | Almacena el valor para Observations en el contexto de WorkOrderHistory. |
| ActionBy | string | NULL |  | Almacena el valor para ActionBy en el contexto de WorkOrderHistory. |
| WorkOrderNavigation | WorkOrder | NOT NULL |  | Almacena el valor para WorkOrderNavigation en el contexto de WorkOrderHistory. |

#### Entidad: **WorkOrderPart**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| WorkOrderId | int | NOT NULL | FK | Almacena el valor para WorkOrderId en el contexto de WorkOrderPart. |
| ProductId | int? | NULL | FK | Almacena el valor para ProductId en el contexto de WorkOrderPart. |
| PartName | string | NULL |  | Almacena el valor para PartName en el contexto de WorkOrderPart. |
| Quantity | int | NOT NULL |  | Almacena el valor para Quantity en el contexto de WorkOrderPart. |
| UnitPrice | decimal | NOT NULL |  | Almacena el valor para UnitPrice en el contexto de WorkOrderPart. |
| IsProvidedByCustomer | bool | NOT NULL |  | Almacena el valor para IsProvidedByCustomer en el contexto de WorkOrderPart. |
| WarrantyEndDate | DateTime? | NULL |  | Almacena el valor para WarrantyEndDate en el contexto de WorkOrderPart. |
| QuotePhotoUrl | string? | NULL |  | Almacena el valor para QuotePhotoUrl en el contexto de WorkOrderPart. |
| IsApproved | bool | NOT NULL |  | Almacena el valor para IsApproved en el contexto de WorkOrderPart. |
| WorkOrderNavigation | WorkOrder | NOT NULL |  | Almacena el valor para WorkOrderNavigation en el contexto de WorkOrderPart. |
| ProductNavigation | Product | NOT NULL |  | Almacena el valor para ProductNavigation en el contexto de WorkOrderPart. |

#### Entidad: **WorkOrderService**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| WorkOrderId | int | NOT NULL | FK | Almacena el valor para WorkOrderId en el contexto de WorkOrderService. |
| Description | string | NULL |  | Almacena el valor para Description en el contexto de WorkOrderService. |
| MechanicId | int | NOT NULL | FK | Almacena el valor para MechanicId en el contexto de WorkOrderService. |
| Price | decimal | NOT NULL |  | Almacena el valor para Price en el contexto de WorkOrderService. |
| EstimatedMinutes | int | NOT NULL |  | Almacena el valor para EstimatedMinutes en el contexto de WorkOrderService. |
| TimeUnit | string | NULL |  | Almacena el valor para TimeUnit en el contexto de WorkOrderService. |
| WarrantyEndDate | DateTime? | NULL |  | Almacena el valor para WarrantyEndDate en el contexto de WorkOrderService. |
| IsApproved | bool | NOT NULL |  | Almacena el valor para IsApproved en el contexto de WorkOrderService. |
| IsPaidToMechanic | bool | NOT NULL |  | Almacena el valor para IsPaidToMechanic en el contexto de WorkOrderService. |
| PaidToMechanicAt | DateTime? | NULL |  | Almacena el valor para PaidToMechanicAt en el contexto de WorkOrderService. |
| MechanicPaymentSettlementId | int? | NULL | FK | Almacena el valor para MechanicPaymentSettlementId en el contexto de WorkOrderService. |
| WorkOrderNavigation | WorkOrder | NOT NULL |  | Almacena el valor para WorkOrderNavigation en el contexto de WorkOrderService. |
| MechanicNavigation | User | NOT NULL |  | Almacena el valor para MechanicNavigation en el contexto de WorkOrderService. |
| MechanicPaymentSettlementNavigation | MechanicPaymentSettlement | NOT NULL |  | Almacena el valor para MechanicPaymentSettlementNavigation en el contexto de WorkOrderService. |

#### Entidad: **Workshop**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| Id | int | NOT NULL | PK | Almacena el valor para Id en el contexto de Workshop. |
| Name | string | NULL |  | Almacena el valor para Name en el contexto de Workshop. |
| Slug | string | NULL |  | Almacena el valor para Slug en el contexto de Workshop. |
| OwnerEmail | string | NULL |  | Almacena el valor para OwnerEmail en el contexto de Workshop. |
| Phone | string? | NULL |  | Almacena el valor para Phone en el contexto de Workshop. |
| Address | string? | NULL |  | Almacena el valor para Address en el contexto de Workshop. |
| City | string? | NULL |  | Almacena el valor para City en el contexto de Workshop. |
| WorkshopType | string | NULL |  | Almacena el valor para WorkshopType en el contexto de Workshop. |
| Plan | string | NULL |  | Almacena el valor para Plan en el contexto de Workshop. |
| IsActive | bool | NOT NULL |  | Almacena el valor para IsActive en el contexto de Workshop. |
| TrialEndsAt | DateTime? | NULL |  | Almacena el valor para TrialEndsAt en el contexto de Workshop. |
| CreatedAt | DateTime | NOT NULL |  | Almacena el valor para CreatedAt en el contexto de Workshop. |
| UpdatedAt | DateTime? | NULL |  | Almacena el valor para UpdatedAt en el contexto de Workshop. |
| Users | ICollection<User> | NOT NULL |  | Almacena el valor para Users en el contexto de Workshop. |
| Settings | ICollection<WorkshopSettings> | NOT NULL |  | Almacena el valor para Settings en el contexto de Workshop. |
| UserRoles | ICollection<UserRole> | NOT NULL |  | Almacena el valor para UserRoles en el contexto de Workshop. |

#### Entidad: **WorkshopSettings**

| Nombre del Campo | Tipo de Dato (C#) | Obligatorio | Llaves | Descripción Funcional Exhaustiva |
|---|---|---|---|---|
| SettingKey | string | NULL |  | Almacena el valor para SettingKey en el contexto de WorkshopSettings. |
| SettingValue | string | NULL |  | Almacena el valor para SettingValue en el contexto de WorkshopSettings. |
| Description | string? | NULL |  | Almacena el valor para Description en el contexto de WorkshopSettings. |
| WorkshopId | int | NOT NULL | FK | Almacena el valor para WorkshopId en el contexto de WorkshopSettings. |
| WorkshopNavigation | Workshop | NOT NULL |  | Almacena el valor para WorkshopNavigation en el contexto de WorkshopSettings. |

