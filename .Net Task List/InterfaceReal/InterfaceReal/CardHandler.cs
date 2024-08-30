using Microsoft.AspNetCore.Http;
using CardLibrary;
using System.Threading.Tasks;
using System.Text.Json;
using InterfaceReal;

public class CardHandler : IHandler
{
    public async Task<object> HandleRequestAsync(HttpContext context)
    {
        await Task.Delay(1000);

        var cardData = new CardData();

        System.Console.WriteLine($"Cardholder Name: {cardData.CardholderName}");
        System.Console.WriteLine($"Card Number: {cardData.CardNumber}");
        System.Console.WriteLine($"Bank Name: {cardData.BankName}");
        System.Console.WriteLine($"Transaction Mode: {cardData.TransactionMode}");
        System.Console.WriteLine($"Card Type: {cardData.CardType}");
        System.Console.WriteLine($"Transaction Type: {cardData.TransactionType}");

        return cardData;
    }
}
