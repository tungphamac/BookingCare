namespace BookingCare.Business.ViewModels
{
    public class SendMessageDto
    {
        public int SenderId { get; set; }    // ID của người gửi
        public int ReceiverId { get; set; }  // ID của người nhận
        public string Content { get; set; }  // Nội dung tin nhắn
    }
}