import { Component } from "@angular/core";

@Component({
  selector: "app-base-validation",
  imports: [],
  template: "",
})
export abstract class BaseValidation {
  abstract errors: Record<string, string>;

  protected setErrors(ex: any) {
    if (ex?.error?.errors?.length) {
      this.errors = {};
      for (const err of ex.error.errors) this.errors[err.key] = err.value;
    }
  }

  protected cleanErrors() {
    this.errors = {};
  }
}
