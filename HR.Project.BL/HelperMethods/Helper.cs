using HR.Project.Entities.Concrete;
using HR.Project.Entities.Enum;

namespace HR.Project.BL.HelperMethods
{
    public class Helper
    {
        public static int IzinOnaylama(int toplamIzinSayisi, int istenenIzinSayisi)
        {
            if (toplamIzinSayisi < istenenIzinSayisi)
            {
                return -1;
            }
            else
            {
                var kalanIzinSayisi = toplamIzinSayisi - istenenIzinSayisi;
                return kalanIzinSayisi;
            }
        }
        public static string AvansTalebi(AdvancePayment payment, UserEntity user, List<AdvancePayment> avanslarim, out bool res)
        {
            res = false;
            double totalAmount = 0;


            if (payment.RequestType == RequestType.Bireysel)
            {
                foreach (var item in avanslarim)
                {

                    if (item.RequestDate.AddYears(1) > DateTime.Now && item.RequestType == RequestType.Bireysel)
                    {
                        totalAmount += item.RequestAmount;

                    }
                }
                double paymentVal = user.Salary * 3;

                double alinabilecekTutar = paymentVal - totalAmount;

                totalAmount += payment.RequestAmount;

                if (totalAmount > user.Salary * 3)
                {

                    return $"Yıl içerisinde en fazla maaşınızın 3 katı kadar avans alabilirsiniz, {alinabilecekTutar} kadar avans hakkınız bulunmakta";

                }
                else
                {
                    res = true;
                    return $"{payment.RequestAmount} tutarında avans talebiniz yöneticinize bildirildi";

                }
            }
            else
            {
                res = true;
                return $"{payment.RequestAmount} {payment.Currency} tutarında avans talebiniz yöneticinize bildirildi";
            }



        }
    }
}
