using AutoMapper;
using Humanizer;
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

        public IdeaCardController(IIdeaCardService ideaCardService, IMapper mapper, IHandleFirebaseService handleFirebaseService)
        {
            _handleFirebaseService = handleFirebaseService;
            _ideaCardService = ideaCardService;
            _mapper = mapper;
        }
        // GET: api/<IdeaCardController>
        [HttpGet]
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

        // POST api/<IdeaCardController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateIdeaCardDTO ideaCardDTO)
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
                    Content = ideaCardDTO.Content
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

        // PUT api/<IdeaCardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IdeaCardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
