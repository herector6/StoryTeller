using System;
using System.Collections.Generic;

namespace StoryTeller.DataAccess.Models;

public partial class StoryTellerModel
{
    public int StoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Story { get; set; } = null!;

    public DateTime? Date { get; set; }

    public byte[]? Image { get; set; }

    public StoryTellerModel(int storyID, string title, string story, DateTime date, byte[] image)
    {
        StoryId = storyID;
        Title = title;
        Story = story;
        Date = date;
        Image = image;
    }
    public StoryTellerModel()
    {

    }
}
