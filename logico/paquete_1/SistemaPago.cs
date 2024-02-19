using System;
namespace logico.paquete_1
{
	public abstract class SistemaPago
	{
		protected double credito { get; set; }
		protected int tiempo { get; set; }
		protected double tasa_interes { get; set; }
		

        public SistemaPago(double pCredito,int pTiempo, double pTasa_interes)
		{
            
            credito = pCredito;
			tasa_interes = validarInteres(pTasa_interes);
			tiempo = pTiempo;
			
		}

        public SistemaPago(double pCredito, int pTiempo, double pTasa_interes, Convertidor convertidor)
        {
            
            credito = convertidor.realizarTipoCambio(pCredito);
            tasa_interes = validarInteres(pTasa_interes) ;
			
            tiempo = pTiempo;
			
        }

        

        public abstract Dictionary<string, List<double>> calcularAmenidadesCredito();

		
		private double validarInteres(double pInteres)
		{
			if (pInteres > 1) { return pInteres / 100; }
			return pInteres; 
		}
	}
}

