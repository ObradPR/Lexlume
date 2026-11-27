import { Routes } from "@angular/router";
import { RouteConstants } from "./constants";

export const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: RouteConstants.ROUTE_HOME,
        title: RouteConstants.TITLE_HOME,
        loadComponent: () => import("./pages/home/home").then((_) => _.Home),
      },
    ],
  },

  //
  //  AUTH
  //
  {
    path: RouteConstants.ROUTE_AUTH,
    children: [
      {
        path: "",
        redirectTo: RouteConstants.ROUTE_LOG_IN,
        pathMatch: "full",
      },
      {
        path: RouteConstants.ROUTE_REGISTER,
        title: RouteConstants.TITLE_REGISTER,
        loadComponent: () => import("./pages/auth/register/register").then((_) => _.Register),
      },
    ],
  },
];
