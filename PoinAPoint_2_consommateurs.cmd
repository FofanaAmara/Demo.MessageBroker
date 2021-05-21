@echo off

cd C:\Users\Ordi\source\repos\Demo.MessageBroker

start "Consommateur 1" Demo.PointAPoint.Consommateur\bin\debug\Demo.PointAPoint.Consommateur.exe 

start "Consommateur 2" Demo.PointAPoint.Consommateur\bin\debug\Demo.PointAPoint.Consommateur.exe 

