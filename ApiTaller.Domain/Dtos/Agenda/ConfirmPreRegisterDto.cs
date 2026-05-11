namespace ApiTaller.Domain.Dtos.Agenda
{
    /// <summary>
    /// DTO para confirmar un pre-registro (Flujo 2), vinculando la cita a un customer existente o recién creado.
    /// </summary>
    public class ConfirmPreRegisterDto
    {
        public int AppointmentId { get; set; }

        /// <summary>
        /// Id del Customer ya existente o recién creado por el Admin.
        /// </summary>
        public int CustomerId { get; set; }

        public int? VehicleId { get; set; }
    }
}
