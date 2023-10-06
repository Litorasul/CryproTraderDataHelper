﻿using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

namespace CryptoTraderDataHelperAPI.Services.DataAccess
{
    public interface ITradeDataAccessService
    {
        Task<int> AddNewTradeAsync(TradeImportDto importDto);
        List<TradeExportDto> GetAllTradesForATimePeriod(DateTime from, DateTime to);
        Task UpdateDailyAverageForATradeAsync(int tradeId, int averageId);
        Task UpdateMinutelyAverageForATradeAsync(int tradeId, int averageId);
        Task UpdateWeeklyAverageForATradeAsync(int tradeId, int averageId);
    }
}