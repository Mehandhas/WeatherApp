import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TemperatureService {
  baseUrl = environment.API_URL;
  constructor(private _http: HttpClient) {
  }

  getCities(): Observable<any> {
    let getCitiesUrl = this.baseUrl + 'getallcities';
    return this._http.get(getCitiesUrl);
  }

  getTemperatureData(startDate: string, endDate: string, cityId: string, variableName: string, unit?: string): Observable<any> {
    let getTemperatureDataUrl = this.baseUrl + 'temperaturetrend?variableName=' + variableName + '&startTimestamp=' + startDate + '&endTimestamp=' + endDate + '&cityId=' + cityId + '&unit=' + unit;
    return this._http.get(getTemperatureDataUrl);
  }

  getHottestCity(): Observable<any> {
    let getHottestCityUrl = this.baseUrl + 'hottestcity';
    return this._http.get(getHottestCityUrl);
  }

  getMoistestCity(): Observable<any> {
    let getMoistestCityUrl = this.baseUrl + 'moistestcity';
    return this._http.get(getMoistestCityUrl);
  }

}
