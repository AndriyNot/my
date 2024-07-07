import { makeAutoObservable } from "mobx";
import { User, UserManager, WebStorageStateStore } from "oidc-client";
import authConfig from "../config";

class AuthStore {
  user: User | null = null;
  userManager: UserManager;

  constructor() {
    makeAutoObservable(this);
    this.userManager = new UserManager({
      ...authConfig,
      userStore: new WebStorageStateStore({ store: window.localStorage }),
    });

    this.userManager.events.addUserLoaded((user) => {
      this.user = user;
    });

    this.userManager.events.addUserUnloaded(() => {
      this.user = null;
    });

    this.getUser();
  }

  async getUser() {
    this.user = await this.userManager.getUser();

    console.log("User >>>>>", this.user);
  }

  login() {
    this.userManager.signinRedirect();
  }

  logout() {
    this.userManager.signoutRedirect();
  }

  async completeLogin() {
    this.user = await this.userManager.signinRedirectCallback();
  }

  async completeLogout() {
    await this.userManager.signoutRedirectCallback();
    this.user = null;
  }
}

// eslint-disable-next-line import/no-anonymous-default-export
export default new AuthStore();
