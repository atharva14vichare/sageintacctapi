# Running the applications.

1) The applications have been developed dontnet version 9. Please make sure to be installed in local machine.
2) Command to run dotnet run
3) Open Postman for testing.
4) http://localhost:5118    This will give APi is running message. Ensuring the the server is connected.
5) http://localhost:5118/api/vendors  . This will provide the intended the vendor details in json format.


# Few Instructions:

1) As Stated in the problem statement, I have atached all necessary code files, Few screenshots, and postman exports in the Zip folder.

2) After few iteration, if you get token expired message while runing. Do create a new token and replace it with the old one in appsettings.json. Lik to create a new token (https://developer.sage.com/intacct/docs/openapi/ap/accounts-payable.vendor/tag/Vendors/#tag/Vendors/operation/get-accounts-payable-vendor-key) {Use the Try it feature}

3) Allte basic functionalities have been completed
    A) Creating webclient API
    B) Creating AuthToken
    C) GetVendors Functionality with error Handiling.
    D) Testing with Postman


Author -- Atharva Vichare (14atharva2000@gmail.com)