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
<br />
![1](https://user-images.githubusercontent.com/43557035/228237636-c255d758-8a55-4b22-b8f0-b8c5fc0ca9ed.png)
<br />
Using the above information, a client application that uses sign-in with Google, knows exactly who the end-user is.
<br />

## Structure of a JWT
A JWT contains three parts:
1. Header: Consists of two parts: 
    - The signing algorithm that’s being used to Encrypt the data.
    - The type of token, which, in this case, is mostly “JWT”.
2. Payload: The payload contains the claims or the JSON object.
3. Signature: A string that is generated via a cryptographic algorithm that can be used to verify the integrity of the JSON payload.

![2](https://user-images.githubusercontent.com/43557035/228240767-b416011f-9cce-4e72-9d03-88387a2b0dd4.png)
<br />

## JWT claim convention
You may have noticed that in the JWT (that is issued by Google) example above, the JSON payload has non-obvious field names. They use sub, iat, aud and so on:
- iss: The issuer of the token (in this case Google)
- azp and aud: Client IDs issued by Google for your application. This way, Google knows which website is trying to use its sign in service, and the website knows that the JWT was issued specifically for them.
- sub: The end user’s Google user ID
- at_hash: The hash of the access token. The OAuth access token is different from the JWT in the sense that it’s an opaque token. The access token’s purpose is so that the client application can query Google to ask for more information about the signed in user.
- email: The end user’s email ID
- email_verified: Whether or not the user has verified their email.
- iat: The time (in milliseconds since epoch) the JWT was created
- exp: The time (in milliseconds since epoch) the JWT was created
- nonce: Can be used by the client application to prevent replay attacks.
- hd: The hosted G Suite domain of the user
<br />
The reason for using these special keys is to follow an industry convention for the names of important fields in a JWT. Following this convention enables client libraries in different languages to be able to check the validity of JWTs issued by any auth servers. For example, if the client library needs to check if a JWT is expired or not, it would simply look for the iat field.

## How do they work (using an example)
The easiest way to explain how a JWT works is via an example. We will start by creating a JWT for a specific JSON payload and then go about verifying it:

### 1) Create a JSON
Let’s take the following minimal JSON payload:
![3](https://user-images.githubusercontent.com/43557035/228242972-29402067-79cf-4ec7-b446-f89e4a67116f.png)

### 2) Create a JWT signing key and decide the signing algorithm
First, we need a signing key and an algorithm to use. We can generate a signing key using any secure random source. For the purpose of this post, let’s use:
- Signing key: 
<span style="color: read"> NTNv7j0TuYARvmNMmWXo6fKvM4o6nv/aUi9ryX38ZH+L1bkrnD1ObOQ8JAUmHCBq7Iy7otZcyAagBLHVKvvYaIpmMuxmARQ97jUVG16Jkpkp1wXOPsrF9zwew6TpczyHkHgX5EuLg2MeBuiT/qJACs1J0apruOOJCg/gOtkjB4c= </span>
