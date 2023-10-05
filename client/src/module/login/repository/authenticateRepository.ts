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
    const formData = new FormData();

    if (auth.email) formData.append("email", auth.email);
    if (auth.password) formData.append("password", auth.password);

    const response = await axios.post("/auth/token", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });

    return response.data;
  };

export { authenticateRepository, AuthenticateRepository };
