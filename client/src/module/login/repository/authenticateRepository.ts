import { AxiosInstance } from "axios";

interface AuthenticateReponse {
  accessToken: string;
  refreshToken: string;
  expirenIn: number;
  tokenType: string;
}

interface AuthenticateRequest {
  email?: string;
  password?: string;
}

interface AuthenticateRepository {
  (auth: AuthenticateRequest): Promise<AuthenticateReponse>;
}

const authenticateRepository =
  (axios: AxiosInstance): AuthenticateRepository =>
  async (auth: AuthenticateRequest) => {
    return await axios.post("/auth/token", auth);
  };

export { authenticateRepository, AuthenticateRepository };
