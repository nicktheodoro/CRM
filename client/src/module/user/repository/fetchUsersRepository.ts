import { TablePagination } from "@/module/pagination/domain/model/pagination";
import { UserPagination } from "@/module/user/domain/model/user";
import { AxiosInstance } from "axios";

interface FetchUsersRepository {
  (pagination: TablePagination): Promise<UserPagination>;
}

const fetchUsersRepository =
  (axios: AxiosInstance): FetchUsersRepository =>
  async (pagination: TablePagination) => {
    const response = await axios.get("/users", {
      params: { page: pagination.page, pageSize: pagination.itemsPerPage },
    });

    return new UserPagination(response);
  };

export { fetchUsersRepository, FetchUsersRepository };
