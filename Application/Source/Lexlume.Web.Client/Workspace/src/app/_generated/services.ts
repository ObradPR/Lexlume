import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IBaseResponseWrapper } from './interfaces';
import { IRegistrationDto } from './interfaces';

@Injectable({ providedIn: 'root' }) export abstract class BaseController
{
	constructor (protected httpClient: HttpClient, protected settingsService: SettingsService) { } 
}
@Injectable({ providedIn: 'root' }) export class AuthController extends BaseController
{
	public Registration(data: IRegistrationDto) : Observable<IBaseResponseWrapper | null>
	{
		const body = <any>data;
		return this.httpClient.post<IBaseResponseWrapper>(
		this.settingsService.createApiUrl('Auth/Registration'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
