namespace Model;

public class Prestamo
{
    
    private string nombreCliente;
    private double monto;
    private int plazo;
    private double porcentajeInteresAnual;
    
    public Prestamo(string nombreCliente, double monto, int plazo, double porcentajeInteresAnual)
    {
        this.nombreCliente= nombreCliente;
        this.monto = monto;
        this.plazo = plazo;
        this.porcentajeInteresAnual = porcentajeInteresAnual;
    }

    public string NombreCliente { get; set; }

    public double Monto { get; set; }

    public int Plazo { get; set; }

    public double PorcentajeInteresAnual { get; set; }
}