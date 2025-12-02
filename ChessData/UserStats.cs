namespace ChessData
{
    /// <summary>
    /// Thống kê người chơi dùng cho màn Profile.
    /// Đây chỉ là DTO – không chứa logic DB.
    /// </summary>
    public class UserStats
    {
        public string Username { get; set; } = "";
        public string IngameName { get; set; } = "";

        public int Rank { get; set; }
        public int HighestRank { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }

        /// <summary>
        /// Tổng số trận đã chơi.
        /// </summary>
        public int TotalGames => Wins + Losses;

        /// <summary>
        /// Tổng thời gian chơi (phút).
        /// </summary>
        public int TotalPlayTimeMinutes { get; set; }

        /// <summary>
        /// Tỉ lệ thắng (0–100).
        /// </summary>
        public double WinRate =>
            TotalGames == 0 ? 0.0 : (double)Wins / TotalGames * 100.0;

        /// <summary>
        /// Danh hiệu của người chơi (dựa trên Rank).
        /// </summary>
        public int TitleId { get; set; }
        public string TitleName { get; set; } = "";
    }
}
