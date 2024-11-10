using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WonderingBookApi.DTOs.IdeaCard;
using WonderingBookApi.Models;
using WonderingBookApi.Services;
using WonderingBookApi.Services.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WonderingBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaCardController : ControllerBase
    {
        private readonly IHandleFirebaseService _handleFirebaseService;
        private readonly IIdeaCardService _ideaCardService;
        private readonly IMapper _mapper;
        private readonly IRedisService _redisService;
        private readonly UserManager<User> _userManager;
        public IdeaCardController(IIdeaCardService ideaCardService, IMapper mapper, IHandleFirebaseService handleFirebaseService, IRedisService redisService, UserManager<User> userManager)
        {
            _handleFirebaseService = handleFirebaseService;
            _ideaCardService = ideaCardService;
            _mapper = mapper;
            _redisService = redisService;
            _userManager = userManager;
        }
        // GET: api/<IdeaCardController>
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var ideaCard = await _ideaCardService.GetAllIdeaCardsAsync();
            return Ok(ideaCard);
        }

        // GET api/<IdeaCardController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdeaCard(Guid id)
        {
            var ideaCard = await _ideaCardService.GetIdeaCardByIdAsync(id);
            if (ideaCard == null)
                return NotFound();

            return Ok(ideaCard);
        }

        [HttpGet("get-by-article/{articleId}")]
        public async Task<IActionResult> GetCardByArticle(Guid articleId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (User.IsInRole("RegularUser"))
            {
                // Check if the user has exceeded the view limit
                bool isWithinLimit = await _redisService.IncrementArticleViews(user.Id);

                if (!isWithinLimit)
                {
                    return NotFound("Daily article view limit reached.");
                }
            }
            // Retrieve the IdeaCard using IdeaCardService
            var ideaCards = await _ideaCardService.GetIdeaCardsByArticleAsync(articleId);
            if (ideaCards == null)
            {
                return NotFound("IdeaCard not found");
            }
            return Ok(ideaCards);
        }

        // POST api/<IdeaCardController>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateIdeaCardDTO ideaCardDTO)
        {
            if (ideaCardDTO == null)
            {
                return BadRequest("IdeaCard data is required.");
            }

            try
            {
                // Create the new IdeaCard using the service
                var newIdeaCard = new IdeaCard
                {
                    ArticleId = ideaCardDTO.ArticleId,
                    CardType = ideaCardDTO.CardType,
                    Title = ideaCardDTO.Title,
                    Content = ideaCardDTO.Content,
                    Order = ideaCardDTO.Order
                };
                newIdeaCard.IdeaCardId = Guid.NewGuid();
                newIdeaCard.Image = await _handleFirebaseService.UploadImageAsync(ideaCardDTO.Image, newIdeaCard.IdeaCardId);

                var createdIdeaCard = await _ideaCardService.CreateIdeaCardAsync(newIdeaCard);

                // Return success with the newly created IdeaCard details
                return CreatedAtAction(nameof(GetIdeaCard), new { id = createdIdeaCard.IdeaCardId }, createdIdeaCard);
            }
            catch (Exception ex)
            {
                // Handle any errors during the creation process
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<IdeaCardController>
        [HttpPost("bulk-create")]
        public async Task<IActionResult> BulkCreate([FromForm] BulkCreateIdeaCardsDTO ideaCardsDTO) //Create new card or update image of existing card
        {
            if (ideaCardsDTO.IdeaCards == null || !ideaCardsDTO.IdeaCards.Any())
            {
                return BadRequest("IdeaCard data is required.");
            }

            var newIdeaCards = await _ideaCardService.BulkCreateIdeaCardAsync(ideaCardsDTO);
            return Ok(newIdeaCards);
        }

        // PUT api/<IdeaCardController>
        [HttpPut("bulk-update")]
        public async Task<IActionResult> BulkUpdate([FromForm] BulkUpdateIdeaCardsDTO ideaCardsDTO) //Does not update image
        {
            if (ideaCardsDTO.IdeaCards == null || !ideaCardsDTO.IdeaCards.Any())
            {
                return BadRequest("IdeaCard data is required.");
            }

            var updateIdeaCards = await _ideaCardService.BulkUpdateIdeaCardAsync(ideaCardsDTO);
            return Ok(updateIdeaCards);
        }

        // PUT api/<IdeaCardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdeaCards(Guid id)
        {
            try
            {
                await _ideaCardService.DeleteIdeaCardsAsync(new List<Guid>() {id});
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<IdeaCardController>/5
        [HttpDelete]
        public async Task<IActionResult> DeleteIdeaCards([FromBody]List<Guid> ideaCards)
        {
            if (ideaCards == null || !ideaCards.Any())
            {
                return BadRequest("IdeaCards id is required.");
            }
            try
            {
                await _ideaCardService.DeleteIdeaCardsAsync(ideaCards);
                return NoContent();
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
