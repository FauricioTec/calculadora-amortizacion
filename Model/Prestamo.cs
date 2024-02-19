namespace Model;

public class Prestamo
{
    public Prestamo(string nombreCliente, double monto, int plazo, double porcentajeInteresAnual)
    {
        this.NombreCliente = nombreCliente;
        this.Monto = monto;
        this.Plazo = plazo;
        this.PorcentajeInteresAnual = porcentajeInteresAnual;
    }

    public string NombreCliente { get; set; }

    public double Monto { get; set; }

    public int Plazo { get; set; }

    public double PorcentajeInteresAnual { get; set; }
}