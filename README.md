# NET-CORE-2-Credentional-Web-Reguest
Credentional Web Reguest with Credential Cache for NET Core 2

Using : CredentialsWebRequest :

    
            var credentialsWebRequest = new CredentialsWebRequest();
            credentialsWebRequest.setCredentionalCache(InterfaceURL, userName, password);
            string ret = credentialsWebRequest.RepeatRequest(dataURL1);
			
		    string ret2 = credentialsWebRequest.RepeatRequest(dataURL2);
			
using : SoapWebReguest

          var soapWebReguest =  new SoapWebReguest();
		  soapWebReguest.Execute(loginURL, strSoupLoginReguest);
		  
		   string ret1 = soapWebReguest.Execute(getDataURL, strSoapGetDataReguest1);
		   
		   string ret2 = soapWebReguest.Execute(getDataURL, strSoapGetDataReguest2);
		  
		  

