using System;
namespace logico.paquete_1
{
	public class SistemaAleman: SistemaPago
	{
        public SistemaAleman(double pCredito, int pTiempo, double pTasa_interes) : base(pCredito, pTiempo, pTasa_interes)
        {

        }

        public SistemaAleman(double pCredito, int pTiempo, double pTasa_interes, Convertidor convertidor) : base(pCredito, pTiempo, pTasa_interes, convertidor)
        {

        }

        public override Dictionary<string, List<double>> calcularAmenidadesCredito()
        {
            Dictionary<string, List<double>> amenidadesCredito = new Dictionary<string, List<double>>(){
                { "Cuota",new List<double>() },
                { "Tasa de interes", new List<double>()},
                { "Deuda Inicial", new List<double>() },
                { "Amortizacion", new List<double>() }
            };

            double cuotaAmortizacion = credito / tiempo;
            double totalInteres = 0;
            double deudaInicial = credito;

            for (int periodo = 1; periodo <= tiempo; periodo++)
            {
                double interes = deudaInicial * tasa_interes;
                double amortizacion = cuotaAmortizacion;

                amenidadesCredito["Cuota"].Add(cuotaAmortizacion + interes);
                amenidadesCredito["Tasa de interes"].Add(interes);
                amenidadesCredito["Deuda Inicial"].Add(deudaInicial);
                amenidadesCredito["Amortizacion"].Add(amortizacion);

                deudaInicial -= amortizacion;
                totalInteres += interes;
            }

            
            amenidadesCredito["Cuota"].Add(amenidadesCredito["Cuota"].Sum());
            amenidadesCredito["Tasa de interes"].Add(amenidadesCredito["Tasa de interes"].Sum());
            amenidadesCredito["Deuda Inicial"].Add(0);
            amenidadesCredito["Amortizacion"].Add(amenidadesCredito["Amortizacion"].Sum());

            return amenidadesCredito;

           


        }


    }
}


