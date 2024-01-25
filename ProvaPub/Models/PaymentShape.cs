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
        //Numero do cartão
        //CVC
        //Data de Validade
        
    }

    public class  paypal : PaymentShape
    {
        //conexão com api do paypal
        //validação
    }

}