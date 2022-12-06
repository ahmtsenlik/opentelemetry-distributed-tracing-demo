
# About project

It is a project developed on how to do distributed tracing with .Net.

It collects trace data with opentelemetry and otlp protocol sends it to jaeger Collector, Collector writes data to elasticsearch and trace data can be traced through jaeger.


## Images

![Apis](https://user-images.githubusercontent.com/85018412/205777116-f72e9796-ff18-45f9-9558-0ff1fa55c9a9.PNG)



## Architect
![architect](https://user-images.githubusercontent.com/85018412/205777218-dcbbdc8e-94b4-4891-be38-627c60dc538c.png)
## Getting Started

### Prerequisites
[Docker](https://www.docker.com/)

### Installation

Clone the project
```bash
https://github.com/ahmtsenlik/opentelemetry-distributed-tracing-demo.git
```
and
 ```bash
  docker compose up
```

Open the project in visual studio and run ServiceA and ServiceB

### Usage
Send a get request to the ServiceA "/WeatherForecast/TestRequest" endpoint

Then view the trace data at http://localhost:16686/
## For more information

[Medium Article](https://medium.com/sabancidx/open-telemetry-ile-tracing-f3bdfdce9fb7)
