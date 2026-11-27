import { Component, signal } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { PageLoader } from "./components/page-loader/page-loader";
import { ToastModule } from "primeng/toast";

@Component({
  selector: "app-root",
  imports: [RouterOutlet, PageLoader, ToastModule],
  templateUrl: "./app.html",
  styleUrl: "./app.css",
})
export class App {
  protected readonly title = signal("Workspace");
}
