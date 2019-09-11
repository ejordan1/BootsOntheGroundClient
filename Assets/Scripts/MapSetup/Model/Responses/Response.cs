using UnityEngine;


//DO NOT HAVE GETTERS AND SETTERS FOR THIS STUFF,
//THAT MAKES IT NOT DESERIALIZABLE, BUT YOU DON'T
//HAVE ANY WAY OF FIRUGING THAT OUT
namespace MyMvcProject.Data
{
	[System.Serializable]
	public class Response
	{

		[SerializeField]
		public string ErrorText;

		[SerializeField]
		public int ErrorCode;

		public Response()
		{
			ErrorCode = 0;
			ErrorText = "okay";
		}


	}

	public class BadRequest : Response
	{
		public BadRequest()
		{
			ErrorText = "Bad Request";
			ErrorCode = 501;
		}

		public BadRequest(string text)
		{
			ErrorText = text;
			ErrorCode = 501;
		}
	}



	public class InvalidLoginRequest : Response
	{
		public InvalidLoginRequest()
		{
			ErrorText = "Invalid Login Request";
			ErrorCode = 502;
		}
	}

}
