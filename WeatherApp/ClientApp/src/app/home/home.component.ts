import { Component } from '@angular/core';
import { Chart } from 'chart.js';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TemperatureService } from '../Service/temperature.Service';
import { DatePipe } from '@angular/common'


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ["./home.component.css"]
})
export class HomeComponent {
  title = 'Weather application';

  temperatureForm: FormGroup;
  name!: string;
  city!: string;
  startDate!: string;
  endDate!: string;
  fromDate = new Date(2010, 0, 1);
  toDate = new Date(2023, 0, 1);
  humidityDays!: number;
  humidityCity!: string;

  averageMoisture!: number;
  moistureCity!: string;

  cities!: Array<any>;
  names: string[] = ['Temperature', 'Humidity'];
  units: any[] = [{ Key: 'C', Value: 'Celsius' }, { Key: 'F', Value: 'Fahrenheit' }];
  chart!: Chart;
  other = [];
  error: any = { isError: false, errorMessage: '' };

  constructor(private _temperatureService: TemperatureService
    , private fb: FormBuilder
    , public datepipe: DatePipe) {
    this.temperatureForm = fb.group({
      'city': [null, Validators.required],
      'startDate': [null, Validators.required],
      'endDate': [null, Validators.required],
      'name': [null, Validators.required],
      'unit': [null]
    });
    this.temperatureForm.controls['unit'].disable()
  }

  ngOnInit() {
    this.getCities();
    this.getHottestCity();
    this.getMoistestCity();
  }


  getCities() {
    this._temperatureService.getCities()
      .subscribe(res => {
        if (res) {
          this.cities = res;
        }
      });
  }

  getHottestCity() {
    this._temperatureService.getHottestCity()
      .subscribe(res => {
        if (res) {
          this.humidityCity = res.cityName;
          this.humidityDays = res.totalDaysAbove30C;
        }
      });
  }

  getMoistestCity() {
    this._temperatureService.getMoistestCity()
      .subscribe(res => {
        if (res) {
          this.moistureCity = res.cityName;
          this.averageMoisture = Math.round(res.averageHumidity);
        }
      });
  }


  onFormSubmit() {
    if (this.temperatureForm.invalid) {
      this.error = { isError: true, errorMessage: 'Select mandatory fields' };
    }
    else if (this.temperatureForm.value.name == "Temperature" && !this.temperatureForm.value.unit) {
      this.error = { isError: true, errorMessage: 'Please select unit' };
    }
    else if (new Date(this.temperatureForm.value.startDate) > new Date(this.temperatureForm.value.endDate)) {
      this.error = { isError: true, errorMessage: 'Start Date should be earlier than End Date' };
    } else {
      this.error = { isError: false, errorMessage: '' };
      let startYear = new Date(this.temperatureForm.value.startDate);
      let endYear = new Date(this.temperatureForm.value.endDate);
      let variableName = this.temperatureForm.value.name;
      let unit = !this.temperatureForm.value.unit ? "" : this.temperatureForm.value.unit;
      let cityId = this.temperatureForm.value.city;
      this.getTemperatureData(this.toDateTimestamp(startYear), this.toDateTimestamp(endYear), cityId, variableName, unit);
    }
  }

  getTemperatureData(startDate: string, endDate: string, cityId: string, variableName: string, unit?: string) {
    this._temperatureService.getTemperatureData(startDate, endDate, cityId, variableName, unit)
      .subscribe(res => {
        if (res) {
          let { dateSet, data } = this.createDatasets(res);
          this.drawingChart(dateSet, data);
        }
      });
  }

  private drawingChart(dateSet: any[], data: any[]) {
    if (this.chart != null) {
      this.chart.destroy();
    }

    let lowCelsius = 25;
    let lowFahrenheit = 50;
    let lowHumidity = 40;

    let unit = this.temperatureForm.value.name == "Temperature" ? "(°" + this.temperatureForm.value.unit + ")" : "%";
    let lowRate = this.temperatureForm.value.unit == "C" ? lowCelsius : this.temperatureForm.value.unit == "F" ? lowFahrenheit : lowHumidity;

    this.chart = new Chart('canvas', {
      type: 'line',
      data: {
        labels: dateSet,
        datasets: [
          {
            label: this.temperatureForm.value.name + unit,
            data: data,
            borderColor: '#3cba9f',
            fill: true,
            borderWidth: 2,
          },
          {
            label: 'Low',
            data: Array(data.length).fill(lowRate),
            fill: true,
            backgroundColor: '#b7a263',
            pointRadius: 0,
          },
          {
            label: 'High',
            data: Array(data.length).fill(Math.max(...data)),
            fill: true,
            backgroundColor: '#eb8484',
            pointRadius: 0
          }
        ]
      },
      options: {
        responsive: true,
        legend: {
          display: true
        },
        scales: {
          xAxes: [{
            display: true
          }],
          yAxes: [{
            display: true,
            ticks: {
              beginAtZero: true
            }
          }],
        }
      },
    });
  }

  private createDatasets(res: object) {
    let temperatureData: any = res;
    let dateSet: any[] = [];
    let data: any[] = [];
    temperatureData.forEach((element: any) => {
      let variableDate = new Date(element.variableTimestamp);
      dateSet.push(this.datepipe.transform(variableDate, 'yyyy-MM-dd'));
      data.push(element.variableValue);

    });
    return { dateSet, data };
  }

  formatDate(date: Date): string {
    const _date = new Date(date);
    const day = _date.getDate();
    const month = _date.getMonth() + 1;
    const year = _date.getFullYear();
    return `${year}-${month}-${day}`;
  }

  formatTime(date: Date): string {
    const _date = new Date(date);
    const hours = _date.getHours()
    const minutes = _date.getMinutes();
    const seconds = _date.getSeconds();
    return `${hours}:${minutes}:${seconds}`;
  }

  toDateTimestamp(date: Date): string {
    const dateStamp = this.formatDate(date);
    const timeStamp = this.formatTime(date);
    return `${dateStamp} ${timeStamp}`;
  }

  nameChange(name: any) {
    if (name.value == "Temperature") {
      this.temperatureForm.controls['unit'].enable();
    }
    else {
      this.temperatureForm.controls['unit'].disable();
      this.temperatureForm.controls['unit'].reset();
    }
  }
}
