namespace BookingCare.Business.ViewModels
{
    public class MessageDetailDto
    {
        public int Id { get; set; }          // ID của tin nhắn
        public int SenderId { get; set; }    // ID của người gửi
        public int ReceiverId { get; set; }  // ID của người nhận
        public string Content { get; set; }  // Nội dung tin nhắn
        public DateTime SentAt { get; set; } // Thời gian gửi
        public bool IsRead { get; set; }     // Trạng thái đã đọc hay chưa
    }
}