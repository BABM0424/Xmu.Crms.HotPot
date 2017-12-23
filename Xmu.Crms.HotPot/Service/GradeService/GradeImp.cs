using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;
using Xmu.Crms.Shared.Exceptions;

namespace Xmu.Crms.Services.HotPot
{
    public class GradeImp : IGradeService
    {
        private readonly CrmsContext _db;

        public GradeImp(CrmsContext db)
        {
            _dc = db;
        }
        public long CountPresentationGrade(long seminarId, long seminarGroupId)
        {
            var count = (from temp in _db.SeminarGroup
                         where Id == seminarGroupId
                         select presentation_grade);
            var count1 = (from count in _db.SeminarGroup where seminarId = seminarId select presentation_grade);
            if (count1 = null)
            {
                throw new ArgumentException();
            }
            return Json(count1);
        }
        public long CountGroupGradeBySeminarId(long seminarId, long seminarGroupId)
        {
            var count = (from temp in _db.SeminarGroup
                         where Id == seminarGroupId
                         select final);
            var count1 = (from count in _db.SeminarGroup where seminarId = seminarId select final_grade);
            if (count1 = null)
            {
                throw new ArgumentException();
            }
            return Json(count1);
        }
        void DeleteStudentScoreGroupByTopicId(long topicId)
        {
			
            var sem = (from temp in _db.Topic
                       where Id = topicId
                       select seminarId);
            if (sem = null)
            {
                throw new ArgumentException();
            }
            foreach (var i in sem)
            {
                var sem1 = (from sem in _db.SeminarGroup
                            where seminarId = sem.seminarId
                            select sem);
                foreach (var j in sem1)
                {
                    _db.SeminarGroup.Remove(j);
                }
            }
            _db.SaveChanges();
        }

    }



}