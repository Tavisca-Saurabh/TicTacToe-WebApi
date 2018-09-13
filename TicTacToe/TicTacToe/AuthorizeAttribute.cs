using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.DataAccess;

namespace TicTacToe
{
    public class AuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        static int playerNumber = 0;
        static string firstToken = "";
        static string secondToken = "";
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["apikey"].ToString();
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new UnauthorizedAccessException("Api Key not passed");
            }
            else
            {
                bool tokenExists = PlayerData.TokenExistsOrNot(apiKey);
                if (!tokenExists)
                {
                    throw new UnauthorizedAccessException("Invalid Access-Token passed");
                }
                else if (tokenExists)
                {
                    if (playerNumber < 2)
                    {
                        playerNumber++;
                    }
                    else if (!apiKey.Equals(firstToken) && !apiKey.Equals(secondToken))
                    {
                        throw new UnauthorizedAccessException("Only Two Players Can Play");
                    }

                    if (playerNumber == 1)
                    {
                        firstToken = apiKey;
                    }
                    else if (!apiKey.Equals(firstToken))
                    {
                        secondToken = apiKey;
                    }
                }
            }
        }
    }
}
