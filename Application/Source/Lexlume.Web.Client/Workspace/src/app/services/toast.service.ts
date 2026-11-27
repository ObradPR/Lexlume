import { Injectable } from "@angular/core";
import { MessageService } from "primeng/api";
import { Constants } from "../constants";

@Injectable({
  providedIn: "root",
})
export class ToastService {
  constructor(private messageService: MessageService) {}

  showSuccess(title?: string, message?: string) {
    this.messageService.add({
      severity: "success",
      summary: title || Constants.SUCCESS,
      detail: message || Constants.SUCCESS_MESSAGE,
    });
  }

  showInfo(title: string, message: string) {
    this.messageService.add({
      severity: "info",
      summary: title,
      detail: message,
    });
  }

  showWarning(title: string, message: string) {
    this.messageService.add({
      severity: "warn",
      summary: title,
      detail: message,
    });
  }

  showError(title?: string, message?: string) {
    this.messageService.add({
      severity: "error",
      summary: title || Constants.ERROR_SERVER,
      detail: message || Constants.ERROR_SERVER_MESSAGE,
    });
  }

  showFormError() {
    this.messageService.add({
      severity: "error",
      summary: Constants.FAILED,
      detail: Constants.FAILED_INPUT_MESSAGE,
    });
  }
}
