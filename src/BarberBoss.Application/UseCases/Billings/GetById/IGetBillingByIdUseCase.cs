﻿using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Billings.GetById;

public interface IGetBillingByIdUseCase
{
    Task<ResponseBillingJson> Execute(long id);
}
