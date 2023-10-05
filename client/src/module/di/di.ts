import axios from "axios";

import { fetchUsersRepository } from "../atendimento/repository/fetchUsersRepository";
import { fetchUsersUseCase } from "../atendimento/domain/useCase/fetchUsersUseCase";
import { createUserRepository } from "../atendimento/repository/createUserRepository";
import { createUserUseCase } from "../atendimento/domain/useCase/createUserUseCase";

import { UserController } from "../atendimento/controller/UserController";

import { authenticateRepository } from "../login/repository/authenticateRepository";
import { authenticateUseCase } from "../login/domain/useCase/authenticateUseCase";
import { AuthenticateController } from "../login/controller/AuthController";

// axios.defaults.headers.common["Accept"] = "*/*";
axios.defaults.headers.common["Content-Type"] =
  "application/json;charset=UTF-8";
axios.defaults.baseURL = "https://localhost:7213/api";

const axiosInstance = axios.create();

axiosInstance.interceptors.response.use(
  (response) => response,
  async (err) => {
    const status = err.response ? err.response.status : null;

    if (status === 500) {
      // Do something here or on any status code return
    }

    return Promise.reject(err);
  }
);

const authenticateController = (context: any) => {
  const authenticateRepositoryImpl = authenticateRepository(axiosInstance);
  const authenticateUseCaseImpl = authenticateUseCase(
    authenticateRepositoryImpl
  );

  return new AuthenticateController(context, authenticateUseCaseImpl);
};

const usuarioController = (context: any) => {
  const fetchUsersRepositoryImpl = fetchUsersRepository(axiosInstance);
  const fetchUsersUseCaseImpl = fetchUsersUseCase(fetchUsersRepositoryImpl);
  const createUserRepositoryImpl = createUserRepository(axiosInstance);
  const createUserUseCaseImpl = createUserUseCase(createUserRepositoryImpl);

  return new UserController(
    context,
    fetchUsersUseCaseImpl,
    createUserUseCaseImpl
  );
};

export { authenticateController, usuarioController };
