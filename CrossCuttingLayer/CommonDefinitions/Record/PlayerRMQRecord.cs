using System;
using System.Collections.Generic;

namespace Reservation.CommonDefinitions.Records
{
    public class PlayerRMQRecord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Location { get; set; }
        public string CoverUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int BuddyRequestId { get; set; }
        public bool IsMyBuddy { get; set; }
        public string Provider { get; set; }
        public int TotalPoints { get; set; }
        public bool IsMyConnection { get; set; }
    }



}