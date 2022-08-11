using UnityEngine;

public class BuyTransit : Transition
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out ShopTable shopTable) && NeedTransit == false)
        {
            shopTable.Accept(Buyer);
            NeedTransit = true;
        }
    }
}
