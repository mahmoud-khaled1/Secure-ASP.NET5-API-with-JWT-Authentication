# Secure-ASP.NET5-API-with-JWT-Authentication
## What is a JWT? Understanding JSON Web Tokens 
JWTs or JSON Web Tokens are most commonly used to identify an authenticated user. They are issued by an authentication server and are consumed by the client-server (to secure its APIs).

## Looking for a breakdown for JSON Web Tokens (JWTs)? You’re in the right place. We will cover:

* What is a JWT?
* Structure of a JWT
* JWT claim conventions
* How do they work (using an example)?
* Pros and Cons of JWTs
* Common issues during development
* Further reading material

## What is JWT
JSON Web Token is an open industry standard used to share information between two entities, usually a client (like your app’s frontend) and a server (your app’s backend).
They contain JSON objects which have the information that needs to be shared. Each JWT is also signed using cryptography (hashing) to ensure that the JSON contents (also known as JWT claims) cannot be altered by the client or a malicious party.
<br />
For example, when you sign in with Google, Google issues a JWT which contains the following claims / JSON payload:
