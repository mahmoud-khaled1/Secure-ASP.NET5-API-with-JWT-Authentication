# Secure-ASP.NET5-API-with-JWT-Authentication
## What is a JWT? Understanding JSON Web Tokens 
JWTs or JSON Web Tokens are most commonly used to identify an authenticated user. They are issued by an authentication server and are consumed by the client-server (to secure its APIs).

## Looking for a breakdown for JSON Web Tokens (JWTs)? You’re in the right place. We will cover:

* What is a JWT?
* Structure of a JWT
* JWT claim conventions
* How do they work (using an example)?
* Pros and Cons of JWTs

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
```diff
NTNv7j0TuYARvmNMmWXo6fKvM4o6nv/aUi9ryX38ZH+L1bkrnD1ObOQ8JAUmHCBq7Iy7otZcyAagBLHVKvvYaIpmMuxmARQ97jUVG16Jkpkp1wXOPsrF9zwew6TpczyHkHgX5EuLg2MeBuiT/qJACs1J0apruOOJCg/gOtkjB4c= 

```
- Signing algorithm: HMAC + SHA256, also known as HS256.

### 3) Creating the “Header”
This contains the information about which signing algorithm is used. Like the payload, this is also a JSON and will be appended to the start of the JWT (hence the name header):
![4](https://user-images.githubusercontent.com/43557035/228245389-e8b51da3-67aa-4be6-8ed0-f12f56395b00.png)

### 4) Create a signature
- First, we remove all the spaces from the payload JSON and then base64 encode it to give us
 ```diff 
  eyJ1c2VySWQiOiJhYmNkMTIzIiwiZXhwaXJ5IjoxNjQ2NjM1NjExMzAxfQ
 ``` 
 You can try pasting this string in an [online base64 decoder](https://www.base64decode.org/) to retrieve our JSON.
 
- Similarly, we remove the spaces from the header JSON and base64 encode it to give us: 
```diff
  eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9
```
- We concatenate both the base 64 strings, with a . in the middle like header.payload , giving us 
```diff
    eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOiJhYmNkMTIzIiwiZXhwaXJ5IjoxNjQ2NjM1NjExMzAxfQ
```
There is no special reason to do it this way other than to set a convention that the industry can follow.
- Now we run the Base64 + HMACSHA256 function on the above concatenated string and the secret to give us the signature:
![5](https://user-images.githubusercontent.com/43557035/228249416-d916893b-3cc8-4c03-bf47-e2dc63bbb3e0.png)
We base64 encode it only as an industry convention.

### 5) Creating the JWT
Finally, we append the generated signature like header.body.signature to create our JWT:
```diff
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOiJhYmNkMTIzIiwiZXhwaXJ5IjoxNjQ2NjM1NjExMzAxfQ.3Thp81rDFrKXr3WrY1MyMnNK8kKoZBX9lg-JwFznR-M
```
### 6) Verifying the JWT
The auth server will send the JWT back to the client’s frontend. The frontend will attach the JWT to network requests to the client’s api layer. The api layer will do the following steps to verify the JWT:
- Fetches the header part of the JWT ``` (eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9) ```
- Does base64 decoding on it to get the plain text JSON: {"typ":"JWT","alg":"HS256"}
- Verifies that the typ field’s value is JWT and the alg is HS256. If not, it would reject the JWT.
- Fetches signing secret key and runs the same Base64URLSafe(HMACSHA256(...)) operation as step number (4) on the header and body of the incoming JWT. Note that if the incoming JWT’s body is different, this step will generate a different signature than in step (4).
- Checks that the generated signature is the same as the signature from the incoming JWT. If it’s not, then the JWT is rejected.
- We base64 decode the body of the JWT ``` eyJ1c2VySWQiOiJhYmNkMTIzIiwiZXhwaXJ5IjoxNjQ2NjM1NjExMzAxfQ``` to give us {"userId":"abcd123","expiry":1646635611301}.
- We reject the JWT if the current time (in milliseconds) is greater than the JSON’s expiry time (since the JWT is expired).
- We can trust the incoming JWT only if it passes all of the checks above.
## Pros and Cons of JWTs
There are quite a few advantages to using a JWT:
- Secure: JWTs are digitally signed using either a secret (HMAC) or a public/private key pair (RSA or ECDSA) which safeguards them from being modified by the client or an attacker.
- Stored only on the client: You generate JWTs on the server and send them to the client. The client then submits the JWT with every request. This saves database space.
- Efficient / Stateless: It’s quick to verify a JWT since it doesn’t require a database lookup. This is especially useful in large distributed systems.

"This article was written and edited by me but belong to " [SuperToken Website](https://supertokens.com/blog/what-is-jwt)
