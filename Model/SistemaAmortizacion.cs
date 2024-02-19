namespace Model;

public interface SistemaAmortizacion
{
    TablaAmortizacion CrearTablaAmortizacion(Prestamo prestamo, double tipoCambio);
}