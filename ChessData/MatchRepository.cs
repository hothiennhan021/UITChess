using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace ChessData
{
    public class MatchRepository
    {
        private readonly string _connectionString;

        public MatchRepository(string conn)
        {
            _connectionString = conn;
        }

        // Cập nhật kết quả trận đấu
        public async Task UpdateMatchResult(string winner, string loser, int minutes)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            // Cộng điểm rank và số trận
            string sql = @"
                UPDATE Users 
                SET 
                    Wins = Wins + CASE WHEN Username = @winner THEN 1 ELSE 0 END,
                    Losses = Losses + CASE WHEN Username = @loser THEN 1 ELSE 0 END,
                    Rank = Rank + CASE WHEN Username = @winner THEN 10 ELSE 0 END,
                    HighestRank = CASE 
                        WHEN Username = @winner AND Rank + 10 > HighestRank 
                            THEN Rank + 10 
                        ELSE HighestRank 
                    END,
                    TotalPlayTimeMinutes = TotalPlayTimeMinutes + @m
                WHERE Username = @winner OR Username = @loser";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@winner", winner);
            cmd.Parameters.AddWithValue("@loser", loser);
            cmd.Parameters.AddWithValue("@m", minutes);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
