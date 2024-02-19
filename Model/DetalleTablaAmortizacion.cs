namespace Model;

public class DetalleTablaAmortizacion
{
    
    private int periodo;
    private double saldo;
    private double amortizacion;
    private double cuota;
    private double intereses;

    public DetalleTablaAmortizacion(int periodo, double saldo, double intereses, double amortizacion, double cuota)
    {
        this.periodo = periodo;
        this.saldo = saldo;
        this.intereses = intereses;
        this.amortizacion = amortizacion;
        this.cuota = cuota;
    }

    public List<object> ToList()
    {
        return new List<object>
        {
            periodo, 
            saldo,
            intereses,
            amortizacion,
            cuota
        };
    }
}