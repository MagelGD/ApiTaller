# Reglas del Asistente IA (Antigravity / Gemini)

## Regla 1: Planificación Obligatoria
Por solicitud explícita del usuario: el asistente **DEBE SIEMPRE** crear un plan de implementación detallado y esperar el "go" (aprobación) del usuario antes de ejecutar cualquier cambio en el código, sin importar qué tan pequeño o sencillo parezca el cambio.

## Regla 2: Bitácora de Cambios y Contexto Obligatoria (`AI_WORKLOG.md`)
Cada vez que el asistente IA realice cambios, tareas o implementaciones en este proyecto, **DEBE SIEMPRE** actualizar el archivo `AI_WORKLOG.md` ubicado en la raíz del repositorio con el siguiente formato cronológico:
* **Fecha y Hora**: Timestamp de la intervención.
* **Objetivo de la Tarea**: Qué se requirió o solucionó.
* **Archivos Afectados**: Lista de archivos creados, modificados o eliminados.
* **Resumen Técnico de la Implementación**: Decisiones tomadas, endpoints/lógica agregada y detalles clave.
* **Estado Actual y Pendientes**: Situación actual del proyecto para que al abrir el proyecto en otro equipo o en una nueva sesión se recupere el contexto inmediatamente y se ahorren tokens.
