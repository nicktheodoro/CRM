import { FetchUsersRepository } from "../../repository/fetchUsersRepository";
import { UserPagination } from "../model/user";
import { DataOptions } from "vuetify";
import { TablePagination } from "@/module/pagination/domain/model/pagination";

interface FetchUsersUseCase {
  (options: DataOptions, search: string): Promise<UserPagination>;
}

const fetchUsersUseCase =
  (repository: FetchUsersRepository): FetchUsersUseCase =>
  async (options: DataOptions, search: string) => {
    const pagination = new TablePagination({
      descending: options.sortDesc.join(","),
      sort: options.sortBy.join(","),
      page: options.page,
      itemsPerPage: options.itemsPerPage,
      search: search,
    });

    const userPagination = await repository(pagination);
    return userPagination;
  };

export { fetchUsersUseCase, FetchUsersUseCase };
