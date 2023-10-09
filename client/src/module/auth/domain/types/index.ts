interface AuthenticateRequest {
  email: string;
  password: string;
}

interface AuthenticateResponse {
  accessToken: string;
  refreshToken: string;
  expirenIn: number;
  tokenType: string;
}

export { AuthenticateRequest, AuthenticateResponse };
