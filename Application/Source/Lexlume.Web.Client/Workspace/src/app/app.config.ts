import { ApplicationConfig, provideBrowserGlobalErrorListeners, Provider } from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routes";
import { MessageService } from "primeng/api";

export const appConfig: ApplicationConfig = {
  providers: [provideBrowserGlobalErrorListeners(), provideRouter(routes), servicesProvider()],
};

function servicesProvider(): Provider[] {
  return [MessageService];
}
