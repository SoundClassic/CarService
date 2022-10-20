using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CarService
{
    /// <summary>
    /// Методы библиотеки
    /// </summary>
    public static class Methods
    {
        public static string GenerateNumberBid()
        {
            Random random = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
            int num = random.Next(1000, 10000);
            string word = "";
            for (byte i = 0; i < 4; i++)
            {
                word += (char)random.Next('A', 'Z' + 1);
            }
            return word + num;
        }

        public static bool IsTimeToComplete(string numberBid)
        {
            using (var access = new Access())
            {
                Bid bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();

                return (DateTime.Now - bid.Date).Minutes >= bid.Type.AlottMinTime;
            }
        }

        public static void AddBid(string numberBid, string LFP, 
            string brand, string typeWork, DateTime date)
        {
            using (var access = new Access())
            {
                Bid bid = new Bid()
                {
                    NumberBid = numberBid,
                    LFP = LFP,
                    Brand = brand,
                    Type = access.TypeWorks
                                 .Where(type => type.Type == typeWork)
                                 .First(),
                    Date = date,
                    Status = Statuses.Active.ToString(),
                    Succes = false
                };
                access.Bids.Add(bid);
                access.SaveChanges();
            }
        }

        private static List<Bid> Get(Predicate<Bid> predicate)
        {
            List<Bid> bids = new List<Bid>();
            using (var access = new Access())
            {
                foreach (var bid in access.Bids.Where(predicate.Invoke))
                {
                    bids.Add(bid);
                }
            }
            return bids;
        }

        private static List<BidDao> Show(Predicate<Bid> predicate)
        {
            List<BidDao> bids = new List<BidDao>();
            foreach (var bid in Get(predicate))
            {
                bids.Add(new BidDao()
                {
                    NumberBid = bid.NumberBid,
                    Date = bid.Date,
                    Status = bid.Status
                });
            }
            return bids;
        }

        public static List<string> GetTypeWorks()
        {
            List<string> types = new List<string>();
            using (var access = new Access())
            {
                foreach (var item in access.TypeWorks)
                {
                    types.Add(item.Type);
                }
            }
            return types;
        }

        public static List<BidDao> GetActiveBids()
        {
            return Show(bid => bid.Status == Statuses.Active.ToString() ||
                               bid.Status == Statuses.InWork.ToString());
        }

        public static List<BidDao> GetHistory()
        {
            return Show(bid => bid.Status == Statuses.Completed.ToString() ||
                               bid.Status == Statuses.Delayed.ToString());
        }

        public static void ChangeBid(string numberBid, string status, string comment)
        {
            using (var access = new Access())
            {
                Bid bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();
                access.Bids.Attach(bid);
                bid.Status = status;
                access.Entry(bid).Property(x => x.Status).IsModified = true;

                if (status == Statuses.Completed.ToString())
                {
                    bid.Succes = true;
                    access.Entry(bid).Property(x => x.Succes).IsModified = true;
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    bid.Comment = comment;
                    access.Entry(bid).Property(x => x.Comment).IsModified = true;
                }
                access.SaveChanges();
            }
        }

        public static BidDao FindBid(string numberBid)
        {
            using (var access = new Access())
            {
                Bid bid = new Bid();
                try
                {
                    bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
                return new BidDao()
                {
                    NumberBid = numberBid,
                    Date = bid.Date,
                    Status = bid.Status
                };
            }
        }

        public static BidDao FindDeleayedBid(string numberBid)
        {
            using (var access = new Access())
            {
                Bid bid;
                try
                {
                    bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid &&
                                       findBid.Status == Statuses.Delayed.ToString())
                                .First();
                    bid.Date = DateTime.Now;
                    bid.Status = Statuses.Active.ToString();
                }
                catch (InvalidOperationException)
                {
                    throw;
                }

                access.Bids.Attach(bid);
                access.Entry(bid).Property(x => x.Date).IsModified = true;
                access.Entry(bid).Property(x => x.Status).IsModified = true;
                access.SaveChanges();
                return new BidDao()
                {
                    NumberBid = numberBid,
                    Date = bid.Date,
                    Status = Statuses.Active.ToString()
                };
            }
        }

        public static List<BidDao> Search(params object[] @params)
        {
            Expression<Predicate<Bid>> lambda;

            if (@params[0].GetType() == typeof(string))
            {
                if(@params.First().ToString().Split(' ').Length == 3)
                {
                    lambda = bid => bid.LFP == @params[0].ToString();
                }
                else
                {
                    lambda = bid => bid.NumberBid == @params[0].ToString();
                }
            }
            else
            {
                lambda = bid => bid.Date.Date == DateTime.Parse(@params[0].ToString()).Date;
            }

            for (byte i = 1; i < @params.Length; i++)
            {
                var @paramsLambda = lambda.Parameters;
                var body = lambda.Body;
                int len = @params[i].ToString().Split(' ').Length;
                if (@params[i].ToString().Split(' ').Length == 3)
                {
                    var newCheck = Expression.Equal(Expression.PropertyOrField(@paramsLambda[0], "LFP"), Expression.Constant(@params[i].ToString()));
                    var fullExpression = Expression.And(body, newCheck);
                    lambda = Expression.Lambda<Predicate<Bid>>(fullExpression, @paramsLambda);
                }
                else
                {
                    var newCheck = Expression.Equal(Expression.PropertyOrField(@paramsLambda[0], "NumberBid"), Expression.Constant(@params[i].ToString()));
                    var fullExpression = Expression.And(body, newCheck);
                    lambda = Expression.Lambda<Predicate<Bid>>(fullExpression, @paramsLambda);
                }
            }
            return Show(lambda.Compile());
        }

        public static string GetInfo(string numberBid)
        {
            using (var access = new Access())
            {
                Bid bid = access.Bids
                                .Where(findBid => findBid.NumberBid == numberBid)
                                .First();
                string info = string.Empty;

                info += $"Номер заявки: {bid.NumberBid}\n\r";
                info += $"ФИО: {bid.LFP}\n\r";
                info += $"Марка автомобиля: {bid.Brand}\n\r";
                info += $"Тип работы: {bid.Type.Type}\n\r";
                info += $"Стоимость работы: {bid.Type.Cost}\n\r рублей";
                info += $"Дата и время: {bid.Date}\n\r";
                info += $"Статус: {bid.Status}\n\r";
                if (!string.IsNullOrEmpty(bid.Comment))
                {
                    info += $"Комментарий: {bid.Comment}\n\r";
                }
                return info;
            }
        }
    }
}
