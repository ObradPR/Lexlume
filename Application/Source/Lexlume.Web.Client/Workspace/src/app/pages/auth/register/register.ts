import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { BaseValidation } from "../../../components/_base/base-validation";
import { LoaderService } from "../../../services/loader.service";
import { ToastService } from "../../../services/toast.service";
import { AuthController } from "../../../_generated/services";
import { DateTime } from "luxon";
import { IRegistrationDto } from "../../../_generated/interfaces";
import { ValidationDirective } from "../../../directives/validation.directive";

@Component({
  selector: "app-register",
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterLink, ValidationDirective],
  templateUrl: "./register.html",
})
export class Register extends BaseValidation implements OnInit {
  override errors: Record<string, string>;
  form: FormGroup;
  minDob: string;
  maxDob: string;

  showPasswords = false;

  constructor(
    private fb: FormBuilder,

    private toastService: ToastService,
    private loaderService: LoaderService,

    private authController: AuthController,
  ) {
    super();
    this.minDob = DateTime.now().minus({ years: 80 }).toISODate();
    this.maxDob = DateTime.now().minus({ years: 14 }).toISODate();
  }

  ngOnInit(): void {
    this.formInit();
  }

  private formInit() {
    this.form = this.fb.group({
      fullName: [""],
      username: [""],
      email: [""],
      password: [""],
      confirmPassword: [""],
      dateOfBirth: [null],
    });
  }

  onRegister() {
    this.loaderService.showPageLoader();

    const data: IRegistrationDto = this.form.value;

    this.authController
      .Registration(data)
      .toPromise()
      .then((_) => {
        if (_?.isSuccess) {
          this.toastService.showSuccess("Success", "Successfully registered");
        }
      })
      .catch((ex) => this.setErrors(ex))
      .finally(() => this.loaderService.hidePageLoader());
  }

  togglePasswordsVisibility() {
    this.showPasswords = !this.showPasswords;
  }
}
