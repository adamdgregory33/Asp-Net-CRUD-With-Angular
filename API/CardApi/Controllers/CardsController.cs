using CardApi.Data;
using CardApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;

        public CardsController(CardsDbContext dbContext)
        {
            this.cardsDbContext = dbContext;
        }

        // Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards =  await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        // Get Single Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (card == null)
                return NotFound($"Card Not Found with id: {id}");

            return Ok(card);
        }

        // Add Single Card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            // Overwrites any user input GUID
            card.Id = Guid.NewGuid();

            await cardsDbContext.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();

            // Creates header for us with details to access the created card
            return CreatedAtAction(nameof(GetCard), new {id = card.Id }, card);

        }

        // Update Single Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCard != null)
            {
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CardholderName = card.CardholderName;
                existingCard.SecureCode = card.SecureCode;

                await cardsDbContext.SaveChangesAsync();
                return Ok(existingCard); 
            }

            return NotFound($"Card Not Found with id: {id}");
        }

        // Delete Single Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCard != null)
            {
                cardsDbContext.Remove(existingCard);
                await cardsDbContext.SaveChangesAsync();

                return Ok(existingCard);
            }

            return NotFound($"Card Not Found with id: {id}");
        }
    }
}
