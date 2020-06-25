# tokenCache
Distributed Token Cache sample implementation.

It's a cache implementation for the STS tokens, and it can used in order to replaces the security token with a much smaller substitute. 

This is especially usefull for the SAML tokens which are carrying the issuer public key, resulting in a quite large token size.

It uses a database store in order to share the actual security tokens between different machines.

Any application that needs to consume bUtility.RemoteTokenCache async methods, must have previously configured HttpClient servicePointManager as following
```
<servicePointManager reusePort="true" dangerousAcceptAnyServerCertificate="true" defaultConnectionLimit="7000"/>
```