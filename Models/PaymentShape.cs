namespace ProvaPub.Models
{
    public abstract class PaymentShape
    {
        
        public void FazPagamento()
        {
            //Faz pagamento
        }
    }

    public class pix : PaymentShape
    {
        //Codigo Pix
    }

    public class creditcard : PaymentShape
    {
        //Numero do cart�o
        //CVC
        //Data de Validade
        
    }

    public class  paypal : PaymentShape
    {
        //conex�o com api do paypal
        //valida��o
    }

}