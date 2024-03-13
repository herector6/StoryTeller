using Microsoft.AspNetCore.Mvc;
using StoryTeller.DataAccess.Context;
using StoryTeller.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using StoryTeller.ViewModel;
using StoryTeller.DataAccess.Repository;
using StoryTeller.RequestModel;


namespace StoryTeller.StoryTellerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryTellerController : ControllerBase
    {
        private readonly StoryTellerRepository _repo;

        public StoryTellerController(StoryTellerRepository context)
        {
            _repo = context;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<StoryTellerModel> CreateStory([FromForm]CreateStoryTellerRequestModel request)
        {
            var storyTellerModel = new StoryTellerModel();
            storyTellerModel.Title = request.Title;
            storyTellerModel.Story = request.Story;
            if (request.Date is null)
            {
                storyTellerModel.Date = DateTime.Now;
            }
            else
            {
                storyTellerModel.Date = request.Date;
            }
            if (request.Image is not null && request.Image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    request.Image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    storyTellerModel.Image = fileBytes;
                }
            }
            var result = await _repo.Create(storyTellerModel);
            return result;
        }
        [HttpPut("Update/{storyID}")]
        public async Task<IActionResult> UpdateStory(int storyID, [FromForm] UpdateStoryTellerRequestModel request)
        {
            var existingStory = await _repo.GetStoryByIdAsync(storyID);

            if (existingStory == null)
            {
                return NotFound();
            }

            existingStory.Title = request.Title;
            existingStory.Story = request.Story;
            // Update other properties as needed

            var updatedStory = await _repo.Update(existingStory);

            return Ok(updatedStory);
        }

        /*public async Task<IActionResult> UpdateStory(int storyID, StoryTellerModel updatedStory)
        {
            if (storyID != updatedStory.StoryId)
            {
                return BadRequest("The storyID in the URL does not match the ID in the request body.");
            }
            var existingStory = await _repo.Update(updatedStory);

            if (existingStory == null)
            {
                return NotFound();
            }
            return Ok(existingStory);
        }*/
        [HttpDelete]
        [Route("Delete/{storyID}")]
        public async Task<IActionResult> DeleteStory(int storyID)
        {
            bool deleted = await _repo.Delete(storyID);

            if (deleted)
            {
                return Ok();

            }
            else
            {
                return BadRequest("Not found on the Data Base");
            }
        }
    }
}



 