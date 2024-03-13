using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoryTeller.DataAccess.Context;
using StoryTeller.DataAccess.Models;

namespace StoryTeller.DataAccess.Repository
{
    public class StoryTellerRepository
    {
        private StoryTellerDbContext _dbContext;

        public StoryTellerRepository(StoryTellerDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
        public async Task<StoryTellerModel> Create(StoryTellerModel storyTellerModel)
        {
            await _dbContext.AddAsync(storyTellerModel);
            await _dbContext.SaveChangesAsync();

            return storyTellerModel;
        }
        public async Task<StoryTellerModel> Update(StoryTellerModel newStoryTellerModel)
        {            
            StoryTellerModel existingStory = await _dbContext.StoryTables.FindAsync(newStoryTellerModel.StoryId);

            if (existingStory == null)
            {                
                return null;
            }
            
            existingStory.Title = newStoryTellerModel.Title;
            existingStory.Story = newStoryTellerModel.Story;
            existingStory.Date = newStoryTellerModel.Date;
            existingStory.Image = newStoryTellerModel.Image;
            
            await _dbContext.SaveChangesAsync();

            return existingStory;
        }
        public async Task<bool> Delete(int storyID)
        {
            StoryTellerModel story = _dbContext.StoryTables.Find(storyID);
            if (story == null)
            {
                return false;
            }

            _dbContext.StoryTables.Remove(story);
            
            await _dbContext.SaveChangesAsync();
            return true;            
                  
        }
        public List<StoryTellerModel> GetAllStories()
        {
            List<StoryTellerModel> storyTellerList = _dbContext.StoryTables.ToList();

            return storyTellerList;
        }
        public async Task<StoryTellerModel> GetStoryByIdAsync(int storyID)
        {
            StoryTellerModel story = await _dbContext.StoryTables.FindAsync(storyID);
            return story;
        }

    }
}
