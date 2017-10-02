# NET-CORE2-Credentional-Web-Request
Credentional Web Request with Credential Cache for NET Core 2

Using : CredentialsWebRequest :

    
            var credentialsWebRequest = new CredentialsWebRequest();
            credentialsWebRequest.setCredentionalCache(InterfaceURL, userName, password);
            string ret = credentialsWebRequest.RepeatRequest(dataURL1);
			
		    string ret2 = credentialsWebRequest.RepeatRequest(dataURL2);
			
using : SoapWebRequest

          var soapWebRequest =  new SoapWebRequest();
		  soapWebReguest.Execute(loginURL, strSoupLoginRequest);
		  
		   string ret1 = soapWebReguest.Execute(getDataURL, strSoapGetDataRequest1);
		   
		   string ret2 = soapWebReguest.Execute(getDataURL, strSoapGetDataRequest2);
		  
		  

