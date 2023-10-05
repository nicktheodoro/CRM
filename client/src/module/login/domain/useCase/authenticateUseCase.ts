import { AuthenticateRepository } from "../../repository/authenticateRepository";
// import { UserPagination } from "../model/user";
// import { DataOptions } from "vuetify";
// import { TablePagination } from "@/module/pagination/domain/model/pagination";

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

interface AuthenticateUseCase {
  (auth: AuthenticateRequest): Promise<AuthenticateReponse>;
}

const authenticateUseCase =
  (repository: AuthenticateRepository): AuthenticateUseCase =>
  async (auth: AuthenticateRequest) => {
    return await repository(auth);
  };

export { authenticateUseCase, AuthenticateUseCase };
