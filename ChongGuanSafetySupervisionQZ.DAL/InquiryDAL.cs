﻿using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class InquiryDAL
    {
        public async Task<ResultData<QZ_Inquiry>> Add(QZ_Inquiry qZ_Inquiry)
        {
            string message = "添加信息失败";

            try
            {
                qZ_Inquiry.InquiryId = Guid.NewGuid().ToString();

                qZ_Inquiry.CreateTime = DateTime.Now.ToString();
                qZ_Inquiry.ModifyTime = DateTime.Now.ToString();

                ModelQZ.DatabaseContext.QZ_Inquiry.Add(qZ_Inquiry);
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }
            catch (Exception ex)
            {

            }
            ResultData<QZ_Inquiry> result = new ResultData<QZ_Inquiry> { IsSuccessed = true, Message = message, Data = qZ_Inquiry };
            return result;
        }

        public ResultData<IEnumerable<QZ_Inquiry>> Qurey(string inquiry = "", string eventId = "", string partyId = "", string createUserId = "", string createDepartmentId = "")
        {

            var query = from e in ModelQZ.DatabaseContext.QZ_Inquiry
                        where (partyId == "" || e.PartyId == partyId) &&
                        (createUserId == "" || e.CreateUserId == createUserId) &&
                        (createDepartmentId == "" || e.CreateDepartmentId == createDepartmentId) &&
                        (eventId == "" || e.EventId == eventId) &&
                        (partyId == "" || e.PartyId == partyId)
                        select e;

            IEnumerable<QZ_Inquiry> data = query.ToList();

            List<QZ_Inquiry> resultData = new List<QZ_Inquiry>();

            foreach (var d in data)
            {
                QZ_Inquiry t = new QZ_Inquiry();
                ReflectionHelper.CopyProperties<QZ_Inquiry>(d, t, new String[] { });
                resultData.Add(t);
            }

            ResultData<IEnumerable<QZ_Inquiry>> result = new ResultData<IEnumerable<QZ_Inquiry>> { IsSuccessed = true, Message = "", Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Inquiry>> Delete(QZ_Inquiry qZ_Inquiry)
        {
            string message = "事件不存在";

            var query = from e in ModelQZ.DatabaseContext.QZ_Inquiry
                        where e.InquiryId == qZ_Inquiry.InquiryId
                        select e;

            QZ_Inquiry data = query.FirstOrDefault();

            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_Inquiry>(qZ_Inquiry, data, new String[] { "InquiryId" });

                data.ModifyTime = DateTime.Now.ToString();
                data.IsDeleteId = 1;

                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_Inquiry> result = new ResultData<QZ_Inquiry> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Inquiry>> Update(QZ_Inquiry qZ_Inquiry)
        {
            string message = "事件不存在";

            var query = from e in ModelQZ.DatabaseContext.QZ_Inquiry
                        where e.InquiryId == qZ_Inquiry.InquiryId
                        select e;

            QZ_Inquiry data = query.FirstOrDefault();

            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_Inquiry>(qZ_Inquiry, data, new String[] { "EventId" });

                data.ModifyTime = DateTime.Now.ToString();

                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_Inquiry> result = new ResultData<QZ_Inquiry> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public ResultData<IEnumerable<QZ_InquiryAndParty>> QueryByCondition(string partyName = "", string partyNumber = "", string talkingType = "")
        {
            var query = from i in ModelQZ.DatabaseContext.QZ_Inquiry
                        join p in ModelQZ.DatabaseContext.QZ_Party
                        on i.PartyId equals p.PartyId
                        where
                            (string.IsNullOrEmpty(partyName) || p.PartyName.Contains(partyName))
                        && (string.IsNullOrEmpty(partyNumber) || p.PartyNumber == partyNumber)
                        && (string.IsNullOrEmpty(talkingType) || i.InquiryTalkType == talkingType)
                        select new QZ_InquiryAndParty
                        {
                            Inquiry = i,
                            Party = p
                        };


            IEnumerable<QZ_InquiryAndParty> data = query.ToList();

            List<QZ_InquiryAndParty> resultData = new List<QZ_InquiryAndParty>();

            foreach (var d in data)
            {
                QZ_InquiryAndParty t = new QZ_InquiryAndParty();
                ReflectionHelper.CopyProperties<QZ_InquiryAndParty>(d, t, new String[] { });
                resultData.Add(t);
            }

            ResultData<IEnumerable<QZ_InquiryAndParty>> result = new ResultData<IEnumerable<QZ_InquiryAndParty>> { IsSuccessed = true, Message = "", Data = data };

            return result;
        }

        public ResultData<IEnumerable<QZ_Inquiry>> QureyByDate(DateTime startDateTime,DateTime endDataTime)
        {
            var query = from i in ModelQZ.DatabaseContext.QZ_Inquiry
                        //where i.InquiryDate >= startDateTime && (i.InquiryDate) <= endDataTime
                        orderby i.InquiryDate
                        select i;

            IEnumerable<QZ_Inquiry> data = query.ToList();

            List<QZ_Inquiry> resultData = new List<QZ_Inquiry>();

            foreach (var d in data)
            {
                QZ_Inquiry t = new QZ_Inquiry();
                ReflectionHelper.CopyProperties<QZ_Inquiry>(d, t, new String[] { });
                resultData.Add(t);
            }

            ResultData<IEnumerable<QZ_Inquiry>> result = new ResultData<IEnumerable<QZ_Inquiry>> { IsSuccessed = true, Message = "", Data = data };

            return result;
        }
    }
}
